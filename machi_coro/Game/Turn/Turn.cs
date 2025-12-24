using Game.Models;

namespace Game.Turn;

public class Turn
{
    private readonly BuildPhase _buildPhase = new();
    private bool ExtraTurn = false;
    //крч наверное надо сюда добавить возможность влиять на игру из вне
    //типо надо добавить ожидание сигналов и тд
    // ну и соотаетсвенно playeraction менять
    public bool DoTurn(Player activePlayer, Player[] players)
    {
        // 1. бросок
        var dice = RollPhase.Roll(activePlayer);
        
        // 2. income
        IncomePhase.Income(activePlayer, players, dice.Sum);

        // 3. строительство
        _buildPhase.Execute(activePlayer);
        
        return activePlayer.IsDoubleCheck && dice.IsDouble;
    }

 
}