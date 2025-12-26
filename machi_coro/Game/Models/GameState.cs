using Game.Models.Dice;
using Game.Models.Enterprises;
using Microsoft.CSharp.RuntimeBinder;

namespace Game.Models;

public class GameState
{
    public int GameId { get; init; }

    public Player.Player CurrentPlayer { get; private set; }
    public CardsMart Mart { get; set; }
    public Phase Phase { get; set; }   // "Roll", "Income", "Build"

    public DiceResult DiceValue { get; set; }
    public string LastAction { get; set; }

    public Player.Player[] Players { get; init; }

    public void NextPlayer() => CurrentPlayer = Players[(CurrentPlayer.Id + 1) % Players.Length];

    public void NextPhase()
    {
        if (Phase is Phase.Build)
        {
            Phase = Phase.Roll;
            NextPlayer();
            return;
        }
        Phase += 1;
    }
}
