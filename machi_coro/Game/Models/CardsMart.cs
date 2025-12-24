using Game.Models.Enterprises;
using Game.Models.PurpleCards;
using Game.Utils;

namespace Game.Models;

public class CardsMart
{
    private static List<Enterprise> Mart = [];

    public CardsMart()
    {
        foreach (var enterprise in JsonRepository<Enterprise>.GetAll()
                     .Where(x => !x.Color.Equals(EnterpriseColors.Purple)))
            for (var i = 0; i < 6; i++)
                Mart.Add(new Enterprise(enterprise));
        for (var i = 0; i < 4; i++)
        {
            Mart.Add(new BusinessCenter());
            Mart.Add(new Stadium());
            Mart.Add(new TvCenter());
        }
    }

    public static bool BuyEnterprise(Player player, Enterprise enterprise)
    {
        if (!Mart.Contains(enterprise)) return false;
        player.TakeMoney(enterprise.Cost);
        player.City.Add(enterprise);
        Mart.Remove(enterprise);
        return true;
    }
}