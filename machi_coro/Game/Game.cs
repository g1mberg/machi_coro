using Game.Models;
using Game.Models.Enterprises;
using Game.Utils;

namespace Game;

public class Game
{
    private int _currentPlayerIndex = 0;
    private readonly Player[] Players = new Player[4];
    private List<Enterprise> Mart = [];

    public void Start()
    {
        for (var i = 0; i < 4; i++)
            Players[i] = new Player();
        
        foreach (var enterprise in EnterpriseFromJson.GetAll())
            for (var i = 0; i < 6; i++)
                Mart.Add(enterprise);
    }
    
}