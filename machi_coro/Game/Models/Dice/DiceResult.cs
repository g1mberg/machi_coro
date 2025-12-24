namespace Game;

public class DiceResult(int sum, bool isDouble)
{
    public int Sum {  get; set; } = sum;
    public bool IsDouble { get; set; } = isDouble;
}