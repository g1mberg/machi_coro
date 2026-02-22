namespace Game.Models.Player;

public enum TurnEffect
{
    // от пурпурных карт — выдаются при срабатывании, отзываются после фазы
    CanSteal,
    CanChange,
    RerollUsed,

    // от достопримечательностей — постоянные
    TwoDice,
    Mall,
    DoubleCheck,
    Reroll
}
