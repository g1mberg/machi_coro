namespace Game.Models.Enterprises;

public class Enterprise
{
    public string Name { get; init; }
    public int Cost { get; init; }
    public int[] CubeResult { get; init; }
    public EnterpriseColors Color { get; init; }
    public EnterpriseType EType { get; init; }
    public EnterpriseType? Foreach { get; init; }
    public int Income { get; init; }

    public int Gain(List<Enterprise> enterprises) =>
        Foreach != null ? enterprises.Where(x => x.EType.Equals(Foreach)).Sum(_ => Income) : Income;
}