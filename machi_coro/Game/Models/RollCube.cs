namespace Game.Models;

public class RollCube
{
    private static Random random = new Random();
    public static int Roll() => (int) random.NextInt64(1, 6);
}