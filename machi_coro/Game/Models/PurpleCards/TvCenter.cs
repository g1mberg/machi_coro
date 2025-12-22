namespace Game.Models.PurpleCards;

public class TvCenter : PurpleCards
{
    public override void Apply(Player activePlayer, List<Player> players)
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