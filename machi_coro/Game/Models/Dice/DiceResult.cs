namespace Game.Models.Dice;

public class DiceResult(int sum, bool isDouble)
{
    public int Sum {  get; init; } = sum;
    public bool IsDouble { get; init; } = isDouble;
}