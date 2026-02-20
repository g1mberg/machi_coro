namespace Game.Models.Dice;

public static class Dice
{
    private static readonly Random Random = new();
    
    
    public static DiceResult Roll(int diceCount)
    {
        if (diceCount == 1)
        {
            var res = Random.Next(1, 7);
            return new DiceResult(res, false, res, 0);
        }

        var res1 = Random.Next(1, 7);
        var res2 = Random.Next(1, 7);

        return new DiceResult(res1 + res2, res1 == res2, res1, res2);
    }
}
