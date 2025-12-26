namespace Game.Models;

public class GameState
{
    public int GameId { get; init; }

    public int CurrentPlayerId { get; set; }
    public CardsMart Mart { get; set; }
    public string Phase { get; set; }   // "Roll", "Income", "Build"

    public int DiceValue { get; set; }
    public string LastAction { get; set; }

    public Player.Player[] Players { get; init; }
}
