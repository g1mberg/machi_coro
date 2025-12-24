using Game.Models.Enterprises;
using Game.Models.Sites;
using Game.Utils;

namespace Game.Models;

public class Player
{
    public int Money { get; private set; }

    public readonly Dictionary<string, Site> Sites;

    public List<Enterprise> City = [];

    public bool IsTwoDices => Sites["Terminal"].IsActivated;
    public bool IsMall => Sites["Mall"].IsActivated;
    public bool IsDoubleCheck => Sites["tvTower"].IsActivated;
    public bool IsReroll => Sites["Park"].IsActivated;

    public bool HasWon() => Sites.Values.All(s => s.IsActivated);


    public Player()
    {
        Money = 3;
        City.Add(JsonRepository<Enterprise>.Get("WheatField"));
        City.Add(JsonRepository<Enterprise>.Get("Bakery"));
        Sites = JsonRepository<Site>.GetDict();
    }
    
    public int TakeMoney(int amount)
    {
        var taken = Money < amount ? Money : amount;
        Money -= taken;
        return taken;
    }

    public void AddMoney(int amount) => Money += amount;
}