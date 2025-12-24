using Game.Models;
using Game.Models.Enterprises;
using Game.Turn;

namespace Game;

public class PlayerAction
{
    public static void Roll(Player player, int dices)
    {
        //проверка на читы
        if (!player.IsTwoDices) Dice.Roll(1); 
        RollPhase.DiceRoll = Dice.Roll(dices);
    }
    
    public static void Reroll(Player player, int dices, bool confirm)
    {
        if (confirm)
        {
            RollPhase.DiceReroll = RollPhase.DiceRoll;
            return;
        }
        RollPhase.DiceReroll = player.IsTwoDices ? Dice.Roll(dices) : Dice.Roll(1);
    }

    public static bool Build(Player player, Enterprise enterprise)
    {
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
              && !otherEnterprise.Color.Equals(EnterpriseColors.Purple))) return false;

        player.City.Remove(enterprise);
        player.City.Add(otherEnterprise);
        otherPlayer.City.Remove(otherEnterprise);
        otherPlayer.City.Add(enterprise);
        return true;
    }
}