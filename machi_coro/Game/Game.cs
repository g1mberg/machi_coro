using Game.Models;
using Game.Models.Player;

namespace Game;

public class Game
{
    public GameState Instance { get; private set; }
    public Game()
    {
        Instance = new GameState();
        for (var i = 0; i < 4; i++)
            Instance.Players[i] = new Player(i, $"Player{i}");

        Instance.Mart = new CardsMart();
        Instance.CurrentPlayer = Instance.Players[0];
    }

    public Game(Game game) => Instance = game.Instance;
}