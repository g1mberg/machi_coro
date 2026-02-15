using Game.Models.Dice;
using Game.Models.Enterprises;
using Microsoft.CSharp.RuntimeBinder;

namespace Game.Models;

public class GameState
{
    public Player.Player CurrentPlayer { get; set; }
    public CardsMart Mart { get; set; } = new CardsMart();
    public Phase Phase { get; set; } = Phase.Roll; // "Roll", "Income", "Build"

    public DiceResult DiceValue { get; set; } = new DiceResult(0, false);
    public string LastAction { get; set; } = "";

    public Player.Player[] Players { get; init; } = new Player.Player[4];

    public void NextPlayer() => CurrentPlayer = Players[(CurrentPlayer.Id + 1) % Players.Length];

    public void NextPhase()
    {
        if (Phase is Phase.Build)
        {
            Phase = Phase.Roll;
            return;
        }
        Phase += 1;
    }
}
