using Game.Models;
using Game.Models.Enterprises;
using Game.Models.PurpleCards;
using Game.Models.Sites;

namespace Game;

public class RollPhase
{   
    
    // ну тут этап броска кубика и подсчет денег идет ww
    public bool Roll(Player activePlayer , List<Player> players)
    {   
        var Park = activePlayer.Sites.OfType<Park>().FirstOrDefault();
        var terminal = activePlayer.Sites.OfType<Terminal>().FirstOrDefault();
        var tvTower = activePlayer.Sites.OfType<TvTower>().FirstOrDefault();
        
        bool rerollUsed = false;
        int diceCount = 1;
        var extraTurn = false;
        
        if (terminal?.IsActivated == true)
        {
            diceCount = 2;
        }

        var result = RollCube.Roll(diceCount);
        int dice = result.Sum;
        bool isDouble = result.IsDouble;
        
        if (Park?.IsActivated == true && !rerollUsed)
        {   
            // //ну тут сделать надо чтобы кнопочка велезала и спрашивала у чела рерол или нет
            // if (activePlayer.WantsReroll(result))
            // {
            //     result = RollCube.Roll(diceCount);
            //     rerollUsed = true;
            // }
        }


        //red
        foreach (var player in players.Where(player => player != activePlayer))
        {
            foreach (var e in player.Enterprises.Where(e => e.Color == EnterpriseColors.Red && e.CubeResult.Contains(dice)))
            {
                int gain = e.Gain(player.Enterprises,activePlayer);
                int taken = activePlayer.TakeMoney(gain);
                player.AddMoney(taken);
            }
        }
        //Green card 
        foreach (var e in activePlayer.Enterprises.Where(e => e.Color == EnterpriseColors.Green && e.CubeResult.Contains(dice)))
        {
            activePlayer.AddMoney(e.Gain(activePlayer.Enterprises,activePlayer));
        }

        //Blue card
        foreach (var player in players)
        {
            foreach (var e in player.Enterprises.Where(e => e.Color == EnterpriseColors.Blue && e.CubeResult.Contains(dice)))
            {
                int gain = e.Gain(player.Enterprises,activePlayer);
                player.AddMoney(gain);
            }
        }
        
        //Purple card
        foreach (var e in activePlayer.Enterprises.OfType<PurpleCards>().Where(e => e.CubeResult.Contains(dice)))
        {
            e.Apply(activePlayer, players);
        }

        
       

        if (tvTower?.IsActivated == true && isDouble)
        {
            extraTurn = true;
        }

        return extraTurn;
    }
}