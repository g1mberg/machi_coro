using Game.Models;

namespace Game;

public class Turn
{
    private readonly BuildPhase _buildPhase = new();
    private readonly RollPhase _rollPhase = new();
    private bool ExtraTurn = false;
    public bool DoTurn(Player activePlayer, List<Player> players)
    {
        // 1. бросок + доход
        ExtraTurn = _rollPhase.Roll(activePlayer, players);

        // 2. строительство
        _buildPhase.Execute(activePlayer);
        
        return ExtraTurn;
    }

 
}