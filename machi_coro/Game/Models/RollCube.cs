namespace Game.Models;

public class RollCube
{
    private static readonly Random _random = new();

    public static int Roll(int diceCount = 1)
    {
        int sum = 0;
        for (int i = 0; i < diceCount; i++)
        {
            sum += _random.Next(1, 7); 
        }
        return sum;
    }
}
