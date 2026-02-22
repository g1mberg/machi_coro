using Game.Models;
using Game.Models.Dice;
using Game.Models.Enterprises;
using Game.Models.Player;
using ProtocolFramework;
using ProtocolFramework.Serializator;
using Server.Lobby;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MachiCoroUI
{
    public partial class Form1
    {
        private void Client_OnPacketRecieve(byte[] raw)
        {
            var packet = XPacket.Parse(raw);
            if (packet == null) return;

            var type = XPacketTypeManager.GetTypeFromPacket(packet);
            Console.WriteLine("PACKET RECEIVED: " + type);

            switch (type)
            {
                case XPacketType.LobbyState:
                    {
                        var playersData = packet.GetValueRaw(0);
                        var lobby = DeserializeLobbyState(playersData);
                        Console.WriteLine($"Players count: {lobby.Players.Count}");
                        BeginInvoke(() => RenderLobby(lobby));
                        break;
                    }

                case XPacketType.Welcome:
                    {
                        BeginInvoke(() =>
                        {
                            ConnectPanel.Visible = false;
                            LobbyPanel.Visible = true;
                            LobbyPanel.BringToFront();
                        });
                        break;
                    }

                case XPacketType.GameStart:
                    {
                        BeginInvoke(() =>
                        {
                            ConnectPanel.Visible = false;
                            LobbyPanel.Visible = false;
                            GamePanel.Visible = true;
                            GamePanel.BringToFront();
                        });
                        break;
                    }

                case XPacketType.Handshake:
                    {
                        int serverMagic = packet.GetValue<int>(1);
                        if (serverMagic != _handshakeMagic - 15)
                            Console.WriteLine("Handshake failed");
                        break;
                    }

                case XPacketType.GameStateUpdate:
                    {
                        var game = DeserializeGameState(packet);
                        BeginInvoke(() => RenderGame(game));
                        break;
                    }

                case XPacketType.YouArePlayer:
                    {
                        _myPlayerId = packet.GetValue<int>(1);
                        break;
                    }

                case XPacketType.TradeProposal:
                    {
                        var mustGive = packet.GetString(1);
                        var willReceive = packet.GetString(2);
                        BeginInvoke(() => ShowTradeProposalDialog(mustGive, willReceive));
                        break;
                    }

                case XPacketType.TradeResponse:
                    break;

                case XPacketType.GameOver:
                    {
                        var winnerName = packet.GetString(1);
                        var winnerId = packet.GetValue<int>(2);
                        BeginInvoke(() => ShowGameOver(winnerName, winnerId));
                        break;
                    }

                case XPacketType.Error:
                    {
                        var msg = packet.GetString(1);
                        BeginInvoke(() => _gameView.LastActionLabel.Text = $"Ошибка: {msg}");
                        ShakeForm();
                        break;
                    }
            }
        }

        private LobbyState DeserializeLobbyState(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);

            var playerCount = br.ReadByte();
            var players = new List<LobbyPlayerState>();

            for (int i = 0; i < playerCount; i++)
            {
                var nameLength = br.ReadByte();
                var nameBytes = br.ReadBytes(nameLength);
                var name = Encoding.UTF8.GetString(nameBytes);
                var isReady = br.ReadBoolean();
                players.Add(new LobbyPlayerState { Name = name, IsReady = isReady });
            }

            return new LobbyState { Players = players };
        }

        private GameState DeserializeGameState(XPacket packet)
        {
            var currentPlayerId = packet.GetValue<int>(1);
            var phase = (Phase)packet.GetValue<int>(2);

            var diceField = packet.GetField(3);
            var diceValue = (diceField?.Contents != null && diceField.FieldSize >= 5)
                ? DiceResult.DeserializeDice(diceField.Contents)
                : new DiceResult(0, false);

            var lastAction = packet.GetString(4);

            var players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                var money = packet.GetValue<int>((byte)(10 + i));
                var name = packet.GetString((byte)(20 + i));
                var cityStr = packet.GetString((byte)(30 + i));
                var sitesStr = packet.GetString((byte)(40 + i));

                var player = new Player(i, name);
                player.AddMoney(money - Player.StartMoney);

                player.City.Clear();
                if (!string.IsNullOrEmpty(cityStr))
                {
                    foreach (var eName in cityStr.Split(','))
                    {
                        if (!string.IsNullOrEmpty(eName))
                            player.City.Add(new Enterprise { Name = eName });
                    }
                }

                if (!string.IsNullOrEmpty(sitesStr))
                {
                    foreach (var pair in sitesStr.Split(','))
                    {
                        var parts = pair.Split(':');
                        if (parts.Length == 2 && parts[1] == "1")
                        {
                            if (player.Sites.ContainsKey(parts[0]))
                            {
                                player.Sites[parts[0]].Activate();
                                player.GrantSiteEffect(parts[0]);
                            }
                        }
                    }
                }

                players[i] = player;
            }

            return new GameState
            {
                CurrentPlayer = players[currentPlayerId],
                Phase = phase,
                DiceValue = diceValue,
                LastAction = lastAction,
                Players = players
            };
        }
    }
}
