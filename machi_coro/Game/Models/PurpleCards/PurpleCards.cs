using Game.Models.Enterprises;

namespace Game.Models.PurpleCards;



//сделал для этих неадекватных карт фиол абстр класс, потому что у них че то слишком сильные механики и тяжело все в классе roll через ifы делать , надеюсь ты поймешь и прочтиаешь
public abstract class PurpleCards(Enterprise other) : Enterprise(other)
{
    public abstract void Apply(Player activePlayer, Player[] players);
}