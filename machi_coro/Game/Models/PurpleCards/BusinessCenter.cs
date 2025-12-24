using Game.Models.Enterprises;
using Game.Utils;

namespace Game.Models.PurpleCards;

public class BusinessCenter(Enterprise other) : PurpleCards(other)
{
    public BusinessCenter() : this(JsonRepository<Enterprise>.Get("BusinessCenter")) {}
    public override void Apply(Player activePlayer, Player[] players)
    {   
        // тут надо добавить выбор цели у с кем поменяться преприятием ого круть)(ww)
        
    }
}