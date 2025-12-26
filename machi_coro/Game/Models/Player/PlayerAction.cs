using Game.Models.Dice;
using Game.Models.Enterprises;
using Game.Models.Enterprises.PurpleCards;

namespace Game.Models.Player;

public class PlayerAction
{
    public static DiceResult Roll(Player player, int dices)
    {
        //проверка на читы
        if (!player.IsTwoDices) Dice.Dice.Roll(1); 
        return Dice.Dice.Roll(dices);
    }

    public static bool Build(Player player, Enterprise enterprise)
    {
        if (enterprise.Color is EnterpriseColors.Purple && player.City.Any(x => x.Name.Equals(enterprise.Name))) 
            return false;
        return player.Money >= enterprise.Cost && CardsMart.BuyEnterprise(player, enterprise);
    }

    public static bool Build(Player player, string siteName)
    {
        var site = player.Sites[siteName];
        //проверка на читы
        if (player.Money < site.Cost || site.IsActivated) return false;
        player.TakeMoney(site.Cost);
        site.Activate();
        return true;
    }

    public static bool Change(Player player, Player otherPlayer, Enterprise enterprise, Enterprise otherEnterprise)
    {
        //проверка на читы
        if (!(player.City.Contains(enterprise)
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
    
    public static void Income(Player activePlayer, Player[] players, int dice)
    {
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