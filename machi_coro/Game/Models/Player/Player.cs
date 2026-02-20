using Game.Models.Enterprises;
using Game.Models.Sites;
using Game.Utils;

namespace Game.Models.Player;

public class Player
{
    public const int StartMoney = 100;
    public int Id { get; set; }
    public string Name { get; set; }
    public int Money { get; private set; }

    public readonly Dictionary<string, Site> Sites;

    public readonly List<Enterprise> City = [];

    public bool IsTwoDices => Sites["Terminal"].IsActivated;
    public bool IsMall => Sites["Mall"].IsActivated;
    public bool IsDoubleCheck => Sites["TvTower"].IsActivated;
    public bool IsReroll => Sites["Park"].IsActivated;
    public bool IsChangeable { get; set; } = false;
    public bool IsStealer { get; set; } = false;

    public bool HasWon() => Sites.Values.All(s => s.IsActivated);


    public Player(int id, string name)
    {
        Id = id;
        Name = name;
        Money = StartMoney;
        City.Add(JsonRepository<Enterprise>.Get("WheatField"));
        City.Add(JsonRepository<Enterprise>.Get("Bakery"));
        Sites = JsonRepository<Site>.GetDict()
            .ToDictionary(kvp => kvp.Key, kvp => new Site { Name = kvp.Value.Name, Cost = kvp.Value.Cost });
    }
    
    public int TakeMoney(int amount)
    {
        var taken = Money < amount ? Money : amount;
        Money -= taken;
        return taken;
    }

    public void AddMoney(int amount) => Money += amount;
}