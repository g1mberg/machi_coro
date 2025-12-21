using Game.Models;
using Game.Models.Enterprises;
using Game.Models.Sites;

namespace Game;

public class Turn
{
    public void DoTurn(Player activePlayer , List<Player> players)
    {
        int diceCount = 1;
        
        var terminal = activePlayer.Sites.OfType<Terminal>().FirstOrDefault();
        if (terminal?.IsActivated == true)
        {
            diceCount = 2;
        }

        int dice = RollCube.Roll(diceCount);

        
        foreach (var player in players.Where(player => player != activePlayer))
        {
            foreach (var e in player.Enterprises
                         .Where(e => e.Color == EnterpriseColors.Red && e.CubeResult.Contains(dice)))
            {
                int gain = e.Gain(player.Enterprises);
                activePlayer.TakeMoney(gain);
                player.AddMoney(gain);
            }
        }
        //Green card 
        foreach (var e in activePlayer.Enterprises.Where(e => e.Color == EnterpriseColors.Green && e.CubeResult.Contains(dice)))
        {
            activePlayer.AddMoney(e.Gain(activePlayer.Enterprises));
        }

        //Blue card
        foreach (var player in players)
        {
            foreach (var e in player.Enterprises
                         .Where(e => e.Color == EnterpriseColors.Blue && e.CubeResult.Contains(dice)))
            {
                int gain = e.Gain(player.Enterprises);
                player.AddMoney(gain);
            }
        }
        
        //Purple card
        foreach (var e in activePlayer.Enterprises.Where(e => e.Color == EnterpriseColors.Purple&& e.CubeResult.Contains(dice)))
        {   
            foreach (var player in players.Where(player => player != activePlayer))
            {
                int taken = player.TakeMoney(e.Gain(activePlayer.Enterprises));
                activePlayer.AddMoney(taken);
            }
        }
    }
}