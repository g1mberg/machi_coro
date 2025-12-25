namespace Game.Models;

public class GameState
{
    public int GameId { get; init; }

    public int CurrentPlayerId { get; init; }
    public string Phase { get; init; }   // "Roll", "Income", "Build"

    public int DiceValue { get; init; }  // 0 если ещё не кидали
    public string LastAction { get; init; }

    public Player[] Players { get; init; }
    public CardsMart Market { get; init; }

    public bool IsGameOver { get; init; }
    public int? WinnerPlayerId { get; init; }
    

}
