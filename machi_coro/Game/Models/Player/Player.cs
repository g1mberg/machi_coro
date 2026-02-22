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

    private readonly HashSet<TurnEffect> _turnEffects = new();
    public bool HasEffect(TurnEffect e) => _turnEffects.Contains(e);
    public void Grant(TurnEffect e)     => _turnEffects.Add(e);
    public void Revoke(TurnEffect e)    => _turnEffects.Remove(e);

    public void GrantSiteEffect(string siteName)
    {
        var effect = siteName switch
        {
            "Terminal" => (TurnEffect?)TurnEffect.TwoDice,
            "Mall"     => TurnEffect.Mall,
            "TvTower"  => TurnEffect.DoubleCheck,
            "Park"     => TurnEffect.Reroll,
            _          => null
        };
        if (effect.HasValue) Grant(effect.Value);
    }

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