namespace Game.Models;

public class RollCube
{
    private static readonly Random _random = new();
    
    
    // сделал так потому что есть достопремечательность  которая чекает что выпал дубль и значит чел ходит еще раз
    public static DiceResult Roll(int diceCount)
    {
        if (diceCount == 1)
        {
            int res = _random.Next(1, 7);
            return new DiceResult(res, false);
        }

        int res1 = _random.Next(1, 7);
        int res2 = _random.Next(1, 7);
        bool isDouble = res1 == res2;

        return new DiceResult(res1 + res2, isDouble);
    }
}
