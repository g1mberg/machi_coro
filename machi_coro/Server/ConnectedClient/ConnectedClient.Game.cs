using Game.Models;
using Game.Models.Dice;
using Game.Models.Enterprises;
using Game.Models.Player;
using ProtocolFramework;
using ProtocolFramework.Serializator;

namespace Shared;

public partial class ConnectedClient
{
    private void ProcessRoll(XPacket packet, bool isReroll = false)
    {
        var instance = Game.Instance;
        var dices = packet.GetValue<int>(1);

        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Roll)
        {
            SendError("Сейчас нельзя кидать кубик");
            return;
        }

        if (isReroll)
        {
            if (!ClientPlayer.HasEffect(TurnEffect.Reroll)) { SendError("Нет возможности перебросить"); return; }
            if (ClientPlayer.HasEffect(TurnEffect.RerollUsed)) { SendError("Переброс уже использован"); return; }
            if (instance.DiceValue.Sum == 0) { SendError("Сначала брось кубик"); return; }
            ClientPlayer.Grant(TurnEffect.RerollUsed);
        }
        else
        {
            ClientPlayer.Revoke(TurnEffect.RerollUsed);
        }

        if (dices == 2 && !ClientPlayer.HasEffect(TurnEffect.TwoDice))
        {
            SendError("Нужен Вокзал для двух кубиков");
            return;
        }

        instance.DiceValue = PlayerAction.Roll(ClientPlayer, dices);
        _server.BroadcastGameState(instance);
    }

    private void ProcessConfirm()
    {
        var instance = Game.Instance;
        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Roll)
        {
            SendError("Сейчас нельзя подтвердить");
            return;
        }

        instance.NextPhase();              // Roll → Income
        ClientPlayer.Revoke(TurnEffect.RerollUsed);
        PlayerAction.Income(ClientPlayer, instance);
        instance.NextPhase();              // Income → Steal/Change/Build (автоскип)

        _server.BroadcastGameState(instance);
    }

    private void ProcessBuild(XPacket packet)
    {
        var enterpriseName = packet.GetString(1);
        var instance = Game.Instance;

        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Build)
        {
            SendError("Сейчас нельзя строить");
            return;
        }

        // Пропуск строительства
        if (string.IsNullOrEmpty(enterpriseName))
        {
            instance.NextPhase();
            if (!(ClientPlayer.HasEffect(TurnEffect.DoubleCheck) && instance.DiceValue.IsDouble))
                instance.NextPlayer();
            instance.DiceValue = new DiceResult(0, false);
            _server.BroadcastGameState(instance);
            return;
        }

        bool hadDoubleCheck = ClientPlayer.HasEffect(TurnEffect.DoubleCheck);

        if (!PlayerAction.TryBuild(ClientPlayer, enterpriseName, Game))
        {
            SendError("Не удалось построить");
            return;
        }

        if (ClientPlayer.HasWon())
        {
            var gameOver = XPacket.Create(XPacketType.GameOver);
            gameOver.SetString(1, ClientPlayer.Name);
            gameOver.SetValue(2, ClientPlayer.Id);
            _server.Broadcast(gameOver);
            Console.WriteLine($"Победитель: {ClientPlayer.Name}. Сервер завершает работу...");
            Task.Delay(1500).ContinueWith(_ => { _server.Stop(); Environment.Exit(0); });
            return;
        }

        instance.NextPhase();
        if (!(hadDoubleCheck && instance.DiceValue.IsDouble))
            instance.NextPlayer();
        instance.DiceValue = new DiceResult(0, false);
        _server.BroadcastGameState(instance);
    }

    private void ProcessSteal(XPacket packet)
    {
        var instance = Game.Instance;
        var targetPlayerId = packet.GetValue<int>(1);

        if (targetPlayerId == -1)
        {
            instance.NextPhase();
            _server.BroadcastGameState(instance);
            return;
        }

        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Steal)
        {
            SendError("Сейчас нельзя воровать");
            return;
        }

        var amount = packet.GetValue<int>(2);
        var target = instance.Players.FirstOrDefault(p => p.Id == targetPlayerId);
        if (target is null) { SendError("Цель не найдена"); return; }

        if (!PlayerAction.TrySteal(ClientPlayer, target, amount))
        {
            SendError("Украсть не получилось");
            return;
        }

        instance.NextPhase();
        _server.BroadcastGameState(instance);
    }

    private void ProcessChange(XPacket packet)
    {
        var instance = Game.Instance;

        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Change)
        {
            SendError("Сейчас нельзя обмениваться");
            return;
        }

        var wants = packet.GetValue<bool>(1);
        if (!wants)
        {
            instance.PendingTradeFromPlayerId = -1;
            instance.NextPhase();
            _server.BroadcastGameState(instance);
            return;
        }

        var fromBuilding = packet.GetString(2);
        var toPlayerId   = packet.GetValue<int>(3);
        var toBuilding   = packet.GetString(4);

        var toPlayer = instance.Players.FirstOrDefault(p => p.Id == toPlayerId);
        if (toPlayer == null) { SendError("Игрок не найден"); return; }
        if (!ClientPlayer.City.Any(e => e.Name == fromBuilding)) { SendError("У тебя нет такого здания"); return; }
        if (!toPlayer.City.Any(e => e.Name == toBuilding)) { SendError("У игрока нет такого здания"); return; }

        var targetClient = _server.ClientsSnapshot().FirstOrDefault(c => c.ClientPlayer?.Id == toPlayerId);
        if (targetClient == null) { SendError("Игрок не в сети"); return; }

        instance.PendingTradeFromPlayerId = ClientPlayer.Id;
        instance.PendingTradeFromBuilding = fromBuilding;
        instance.PendingTradeToPlayerId   = toPlayerId;
        instance.PendingTradeToBuilding   = toBuilding;

        var proposal = XPacket.Create(XPacketType.TradeProposal);
        proposal.SetString(1, toBuilding);
        proposal.SetString(2, fromBuilding);
        proposal.SetValue(3, ClientPlayer.Id);
        targetClient.QueuePacketSend(proposal.ToPacket());

        instance.LastAction = $"{ClientPlayer.Name} предлагает обмен {toPlayer.Name}...";
        _server.BroadcastGameState(instance);
    }

    private void ProcessTradeResponse(XPacket packet)
    {
        var instance = Game.Instance;

        if (instance.PendingTradeFromPlayerId == -1 || instance.Phase != Phase.Change)
            return;
        if (instance.PendingTradeToPlayerId != ClientPlayer.Id)
            return;

        var accepted = packet.GetValue<bool>(1);
        if (accepted)
        {
            var fromPlayer = instance.Players[instance.PendingTradeFromPlayerId];
            PlayerAction.TryChange(fromPlayer, ClientPlayer,
                instance.PendingTradeFromBuilding!, instance.PendingTradeToBuilding!);
        }

        instance.PendingTradeFromPlayerId = -1;
        instance.PendingTradeFromBuilding = null;
        instance.PendingTradeToPlayerId   = -1;
        instance.PendingTradeToBuilding   = null;

        instance.NextPhase();
        _server.BroadcastGameState(instance);
    }
}
