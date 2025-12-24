
using Game.Models.Sites;
using Game.Utils;

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
    

    public Enterprise(Enterprise other)
    {
        Name = other.Name;
        Cost = other.Cost;
        CubeResult = other.CubeResult.ToArray(); // глубокая копия массива
        Color = other.Color;
        EType = other.EType;
        Foreach = other.Foreach;
        Income = other.Income;
    }
    
    
    // переписал под торговый центр ебанный без негатива
    public int Gain(List<Enterprise> enterprises, Player owner)
    {
        //считает количество income
        var count = Foreach != null ? enterprises.Count(x => x.EType == Foreach) : 1;
        
        if (owner.IsMall && EType is EnterpriseType.Shop or EnterpriseType.Cafe)
            return Income * count + 1;

        return count * Income;
    }
}