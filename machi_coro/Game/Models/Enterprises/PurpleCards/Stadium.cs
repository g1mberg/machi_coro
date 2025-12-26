using Game.Utils;

namespace Game.Models.Enterprises.PurpleCards;

public class Stadium(Enterprise other) : PurpleCard(other)
{
    public Stadium() : this(JsonRepository<Enterprise>.Get("Stadium")) {}
    public override void Apply(Player.Player activePlayer, Player.Player[] players)
    {
        foreach (var player in players.Where(player => player != activePlayer ))
            activePlayer.AddMoney(player.TakeMoney(2));
    }
    
}