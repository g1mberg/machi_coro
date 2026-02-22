using Game.Models.Player;

namespace Game.Models.Enterprises;

public class Enterprise
{
    public string Name { get; init; }
    public int Cost { get; init; }
    public int[] CubeResult { get; init; }
    public EnterpriseColors Color { get; init; }
    public EnterpriseType EType { get; init; }
    public EnterpriseType? IncomeType { get; init; }
    public int Income { get; init; }

    public Enterprise() { } 

    public Enterprise(Enterprise other)
    {
        Name = other.Name;
        Cost = other.Cost;
        CubeResult = other.CubeResult.ToArray();
        Color = other.Color;
        EType = other.EType;
        IncomeType = other.IncomeType;
        Income = other.Income;
    }

    public int Gain(List<Enterprise> enterprises, Player.Player owner)
    {
        var count = IncomeType != null ? enterprises.Count(x => x.EType == IncomeType) : 1;

        if (owner.HasEffect(TurnEffect.Mall) && EType is EnterpriseType.Shop or EnterpriseType.Cafe)
            return Income * count + 1;

        return count * Income;
    }
}
