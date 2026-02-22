using Game.Models.Dice;
using Game.Models.Enterprises;
using Game.Models.Enterprises.PurpleCards;
using Game.Models.Player;

namespace Game.Models;

public class GameState
{
    public Player.Player CurrentPlayer { get; set; }
    public CardsMart Mart { get; set; } = new CardsMart();
    public Phase Phase { get; set; } = Phase.Roll;

    public DiceResult DiceValue { get; set; } = new DiceResult(0, false);
    public string LastAction { get; set; } = "";

    public Player.Player[] Players { get; init; } = new Player.Player[4];

    public int PendingTradeFromPlayerId { get; set; } = -1;
    public string? PendingTradeFromBuilding { get; set; }
    public int PendingTradeToPlayerId { get; set; } = -1;
    public string? PendingTradeToBuilding { get; set; }

    public void NextPlayer() => CurrentPlayer = Players[(CurrentPlayer.Id + 1) % Players.Length];

    public void NextPhase()
    {
        if (Phase is Phase.Build)
        {
            Phase = Phase.Roll;
            return;
        }
        do
        {
            Phase += 1;
        } while (Phase is not Phase.Build && ShouldSkip());
    }

    private bool ShouldSkip() => Phase switch
    {
        Phase.Steal => !CurrentPlayer.HasEffect(TurnEffect.CanSteal)
                       || !CurrentPlayer.City.OfType<PurpleCard>()
                             .Any(c => c.GrantedEffect == TurnEffect.CanSteal
                                    && c.CubeResult.Contains(DiceValue.Sum)),
        Phase.Change => !CurrentPlayer.HasEffect(TurnEffect.CanChange)
                        || !CurrentPlayer.City.OfType<PurpleCard>()
                              .Any(c => c.GrantedEffect == TurnEffect.CanChange
                                     && c.CubeResult.Contains(DiceValue.Sum)),
        _ => false
    };
}
