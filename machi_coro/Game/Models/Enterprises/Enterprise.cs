
using Game.Models.Sites;

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
    
    public void Build(Player player)
    {
        player.Enterprises.Add(this);
    }

    
    // перепесал под торговый центр ебанный без негатива
    public int Gain(List<Enterprise> enterprises, Player owner)
    {
        int count = Foreach != null ? enterprises.Count(x => x.EType == Foreach) : 1;

        int income = Income;
        
        var mall = owner.Sites.OfType<Mall>().FirstOrDefault();
        if (mall?.IsActivated == true && (EType == EnterpriseType.Shop || EType == EnterpriseType.Cafe))
        {
            income += 1;
        }

        return count * income;
    }

    
    

}