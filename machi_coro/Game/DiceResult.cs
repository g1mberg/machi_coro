namespace Game;

public class DiceResult
{
    public int Sum {  get; set; }
    public bool IsDouble { get; set; }

    public DiceResult(int sum, bool isDouble)
    {
        Sum = sum;
        IsDouble = isDouble;
    }
}