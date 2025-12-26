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
            Instance.Players[i] = new Player();

        Instance.Mart = new CardsMart();
    }

    public Game(Game game) => Instance = game.Instance;
}