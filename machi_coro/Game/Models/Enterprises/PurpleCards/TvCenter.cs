using Game.Models.Player;
using Game.Utils;

namespace Game.Models.Enterprises.PurpleCards;

public class TvCenter(Enterprise other) : PurpleCard(other)
{
    public TvCenter() : this(JsonRepository<Enterprise>.Get("TvCenter")) {}
    public override TurnEffect? GrantedEffect => TurnEffect.CanSteal;
}
