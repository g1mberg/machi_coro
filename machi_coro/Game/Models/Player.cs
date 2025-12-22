using Game.Models.Enterprises;
using Game.Models.Sites;
using Game.Utils;

namespace Game.Models;

public class Player
{
    public int Money { get; private set; }

    public Site[] Sites =
    [
        new Terminal(),
        new Mall(),
        new TvTower(),
        new Park()
    ];

    public List<Enterprise> Enterprises = [];

    private bool _isTerminal = false;
    private bool _isMall = false;
    private bool _isTvTower = false;
    private bool _isPark = false;
    
    public bool HasWon()
    {
        return Sites.All(s => s.IsActivated);
    }


    public Player()
    {
        Money = 3;
        Enterprises.Add(EnterpriseFromJson.Get("WheatField"));
        Enterprises.Add(EnterpriseFromJson.Get("Bakery"));
    }
    
    // оцени говно код от 1 до 10 (давай перепеши это в 1 строку как умеешь)
    public int TakeMoney(int amount)
    {
        int taken = 0;
        if (Money - amount < 0)
        {
            taken = Money;
            Money = 0;
        }
        else
        {
            Money -= amount;
        }
        return taken;
    }

    public void AddMoney(int amount)
    {
        Money += amount;
    }
}