using Game.Models.Enterprises;
using Game.Utils;

namespace Game.Models.PurpleCards;

public class Stadium(Enterprise other) : PurpleCards(other)
{
    public Stadium() : this(JsonRepository<Enterprise>.Get("Stadium")) {}
    public override void Apply(Player activePlayer, Player[] players)
    {
        foreach (var player in players.Where(player => player != activePlayer ))
            activePlayer.AddMoney(player.TakeMoney(2));
    }
    
}