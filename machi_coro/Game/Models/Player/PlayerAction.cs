using Game.Models.Dice;
using Game.Models.Enterprises;
using Game.Models.Enterprises.PurpleCards;
using Game.Models.Sites;

namespace Game.Models.Player;

public static class PlayerAction
{
    public static DiceResult Roll(Player player, int dices)
    {
        //проверка на читы
        if (!player.IsTwoDices) Dice.Dice.Roll(1); 
        return Dice.Dice.Roll(dices);
    }

    public static bool TryBuild(Player player, string building, Game game)
    {
        if (player.Sites.TryGetValue(building, out var site)) return TryBuild(player, site);
        var enterprise = game.Instance.Mart.GetByName(building);
        if (enterprise is null || enterprise.Color is EnterpriseColors.Purple && player.City.Any(x => x.Name.Equals(enterprise.Name))) 
            return false;
        return player.Money >= enterprise.Cost && game.Instance.Mart.BuyEnterprise(player, enterprise);
    }

    public static bool TryBuild(Player player, Site site)
    {
        //проверка на читы
        if (player.Money < site.Cost || site.IsActivated) return false;
        player.TakeMoney(site.Cost);
        site.Activate();
        return true;
    }

    public static bool TryChange(Player player, Player otherPlayer, string enterpriseName, string otherEnterpriseName)
    {
        var enterprise = player.City.FirstOrDefault(x => x.Name.Equals(enterpriseName));
        var otherEnterprise = otherPlayer.City.FirstOrDefault(x => x.Name.Equals(otherEnterpriseName));
        
        //проверка на читы
        if (!(enterprise is not null 
              && otherEnterprise is not null 
              && player.City.Contains(enterprise)
              && otherPlayer.City.Contains(otherEnterprise)
              && !enterprise.Color.Equals(EnterpriseColors.Purple)
              && !otherEnterprise.Color.Equals(EnterpriseColors.Purple) 
              && player.IsChangeable)) return false;

        player.City.Remove(enterprise);
        player.City.Add(otherEnterprise);
        otherPlayer.City.Remove(otherEnterprise);
        otherPlayer.City.Add(enterprise);
        return true;
    }

    public static bool TrySteal(Player player, Player otherPlayer, int sum = 3)
    {
        if (!player.IsStealer) return false;
        player.AddMoney(otherPlayer.TakeMoney(sum));
        return true;
    }
    
    public static void Income(Player activePlayer, GameState game)
    {
        var players = game.Players;
        var dice = game.DiceValue.Sum;
        //red
        foreach (var player in players.Where(player => player != activePlayer))
        foreach (var e in player.City.Where(e => e.Color == EnterpriseColors.Red && e.CubeResult.Contains(dice)))
        {
            var gain = e.Gain(player.City, activePlayer);
            var taken = activePlayer.TakeMoney(gain);
            player.AddMoney(taken);
        }

        //Green card 
        foreach (var e in activePlayer.City.Where(e => e.Color == EnterpriseColors.Green && e.CubeResult.Contains(dice)))
            activePlayer.AddMoney(e.Gain(activePlayer.City, activePlayer));

        //Blue card
        foreach (var player in players)
        foreach (var e in player.City.Where(e => e.Color == EnterpriseColors.Blue && e.CubeResult.Contains(dice)))
            player.AddMoney(e.Gain(player.City, player));

        //Purple card
        foreach (var e in activePlayer.City.OfType<PurpleCard>().Where(e => e.CubeResult.Contains(dice)))
            e.Apply(activePlayer, players);
    }
}