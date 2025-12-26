namespace Game.Models.Enterprises.PurpleCards;

public abstract class PurpleCard(Enterprise other) : Enterprise(other)
{
    public abstract void Apply(Player.Player activePlayer, Player.Player[] players);
}