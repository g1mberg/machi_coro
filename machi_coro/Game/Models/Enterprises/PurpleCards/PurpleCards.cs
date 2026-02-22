using Game.Models.Player;

namespace Game.Models.Enterprises.PurpleCards;

public abstract class PurpleCard(Enterprise other) : Enterprise(other)
{
    public virtual TurnEffect? GrantedEffect => null;
    public virtual void Apply(Player.Player activePlayer, Player.Player[] players) { }
}