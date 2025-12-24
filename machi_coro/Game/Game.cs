using Game.Models;
using Game.Models.Enterprises;
using Game.Utils;

namespace Game;

public class Game
{
    private int _currentPlayerIndex = 0;
    private readonly Player[] Players = new Player[4];
    private CardsMart Mart;
    private readonly Turn.Turn _turn = new();
    private bool _isGameOver = false;


    public void Start()
    {
        for (var i = 0; i < 4; i++)
            Players[i] = new Player();

        Mart = new CardsMart();
        Play();
    }
    
    //тут сделал что игрок ходит
    private void PlayStep()
    {
        var activePlayer = Players[_currentPlayerIndex];
        var extraTurn = _turn.DoTurn(activePlayer, Players);
        if (activePlayer.HasWon())
        {
            EndGame(activePlayer);
            return;
        }
        if (!extraTurn)
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % Players.Length;
        }
    }
    
    //ну тут понятно
    private void Play()
    {
        while (!_isGameOver)
        {
            PlayStep();
        }
    }
    private void EndGame(Player winner)
    {   
        Console.WriteLine($"Игра окончена." + winner + " is winner");
    }


}