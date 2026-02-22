using Game.Models.Player;
using Game.Utils;

namespace Game.Models.Enterprises.PurpleCards;

public class BusinessCenter(Enterprise other) : PurpleCard(other)
{
    public BusinessCenter() : this(JsonRepository<Enterprise>.Get("BusinessCenter")) {}
    public override void Apply(Player.Player activePlayer, Player.Player[] players) => activePlayer.Grant(TurnEffect.CanChange);
}