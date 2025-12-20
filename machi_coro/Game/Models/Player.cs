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


    public Player()
    {
        Money = 3;
        Enterprises.Add(EnterpriseFromJson.Get("WheatField"));
        Enterprises.Add(EnterpriseFromJson.Get("Bakery"));
    }
}