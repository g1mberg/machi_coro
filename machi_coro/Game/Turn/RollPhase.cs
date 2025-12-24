using Game.Models;
using Game.Models.Enterprises;
using Game.Models.PurpleCards;

namespace Game.Turn;

public static class RollPhase
{
    public static DiceResult? DiceRoll = null;
    public static DiceResult? DiceReroll = null;
    
    // ну тут этап броска кубика и подсчет денег идет ww
    // теперь тут только ролл
    public static DiceResult Roll(Player activePlayer)
    {
        while (DiceRoll is null) { }

        if (activePlayer.IsReroll)
            while (DiceReroll is null) { }
        else
        {
            return DiceRoll;
        }

        return DiceReroll;
    }
}