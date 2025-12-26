namespace Game.Models.Enterprises;

public class Enterprise(Enterprise other)
{
    public string Name { get; init; } = other.Name;
    public int Cost { get; init; } = other.Cost;
    public int[] CubeResult { get; init; } = other.CubeResult.ToArray();
    public EnterpriseColors Color { get; init; } = other.Color;
    private EnterpriseType EType { get; init; } = other.EType;
    private EnterpriseType? IncomeType { get; init; } = other.IncomeType;
    private int Income { get; init; } = other.Income;


    public int Gain(List<Enterprise> enterprises, Player.Player owner)
    {
        var count = IncomeType != null ? enterprises.Count(x => x.EType == IncomeType) : 1;
        
        if (owner.IsMall && EType is EnterpriseType.Shop or EnterpriseType.Cafe)
            return Income * count + 1;

        return count * Income;
    }
}