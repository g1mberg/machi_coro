namespace Game.Models.PurpleCards;

public class Stadium : PurpleCards
{
    public override void Apply(Player activePlayer, List<Player> players)
    {
        foreach (var player in players.Where(player => player != activePlayer ))
        {
            int taken = player.TakeMoney(2);
            activePlayer.AddMoney(taken);
        }
    }
    
}