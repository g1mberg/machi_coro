using Game.Models.Enterprises;
using Game.Models.Enterprises.PurpleCards;
using Game.Utils;

namespace Game.Models;

public class CardsMart
{
    private readonly List<Enterprise> _mart = [];
    public IReadOnlyList<Enterprise> GetAvailable() =>  _mart;

    public CardsMart()
    {
        foreach (var enterprise in JsonRepository<Enterprise>.GetAll()
                     .Where(x => !x.Color.Equals(EnterpriseColors.Purple)))
            for (var i = 0; i < 6; i++)
                _mart.Add(new Enterprise(enterprise));
        for (var i = 0; i < 4; i++)
        {
            _mart.Add(new BusinessCenter());
            _mart.Add(new Stadium());
            _mart.Add(new TvCenter());
        }
    }

    public bool BuyEnterprise(Player.Player player, Enterprise? enterprise)
    {
        if (enterprise is null || !_mart.Contains(enterprise)) return false;
        player.TakeMoney(enterprise.Cost);
        player.City.Add(enterprise);
        _mart.Remove(enterprise);
        return true;
    }

    public Enterprise? GetByName(string name) => _mart.FirstOrDefault(x => x.Name.Equals(name));
}