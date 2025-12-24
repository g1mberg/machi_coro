using Game.Models;
using Game.Models.Enterprises;
using Game.Models.PurpleCards;

namespace Game.Turn;

public static class IncomePhase
{
    public static void Income(Player activePlayer, Player[] players, int sum)
    {
        //red
        foreach (var player in players.Where(player => player != activePlayer))
        foreach (var e in player.City.Where(e => e.Color == EnterpriseColors.Red && e.CubeResult.Contains(sum)))
        {
            var gain = e.Gain(player.City, activePlayer);
            var taken = activePlayer.TakeMoney(gain);
            player.AddMoney(taken);
        }

        //Green card 
        foreach (var e in activePlayer.City.Where(e => e.Color == EnterpriseColors.Green && e.CubeResult.Contains(sum)))
            activePlayer.AddMoney(e.Gain(activePlayer.City, activePlayer));

        //Blue card
        foreach (var player in players)
        foreach (var e in player.City.Where(e => e.Color == EnterpriseColors.Blue && e.CubeResult.Contains(sum)))
        {
            var gain = e.Gain(player.City, player);
            player.AddMoney(gain);
        }

        //Purple card
        foreach (var e in activePlayer.City.OfType<PurpleCards>().Where(e => e.CubeResult.Contains(sum)))
            e.Apply(activePlayer, players);
    }
}