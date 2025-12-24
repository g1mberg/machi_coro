using Game.Models.Enterprises;
using Game.Utils;

namespace Game.Models.PurpleCards;

public class TvCenter(Enterprise other) : PurpleCards(other)
{
    public TvCenter() : this(JsonRepository<Enterprise>.Get("TvCenter")) {}
    public override void Apply(Player activePlayer, Player[] players)
    {   
        // тут надо добавить выбор цели у кого красть деньги ого ww)(
        // var target = players
        //     .Where(p => p != activePlayer)
        //     .OrderByDescending(p => p.Money)
        //     .FirstOrDefault();
        //
        // if (target == null) return;
        //
        // int taken = target.TakeMoney(Income);
        // activePlayer.AddMoney(taken);
    }
}