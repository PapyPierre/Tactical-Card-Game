using System;
using Board;
using Cards;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    private BoardManager _boardManager;
    private UIManager _uiManager;

    private bool gameHasStarted;
    
    private int turnNumber;

    public int TurnNumber
    {
        get => turnNumber;

        set
        {
            turnNumber = value;
            _uiManager.turnNumberTMP.text = "Turn " + value;
        }
    }
    
    [HideInInspector] public bool gameIsFinish;
    
    public Player[] players;
   // [HideInInspector] 
    public Player currentPlayer;
    
    private void Start()
    {
        _boardManager = BoardManager.instance;
        _uiManager = UIManager.instance;
    }
    
    public void StartGame()
    {
        _boardManager.Init();
        currentPlayer = players[0];
        currentPlayer.StartTurn(true);
        gameHasStarted = true;
        gameIsFinish = false;
    }
    
    private void Update()
    {
        if (!gameHasStarted) return;
       
        if (Input.GetMouseButtonDown(0) && currentPlayer.isReadyToPlay)
        {
            OnPlayerClick();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            gameIsFinish = true;
        }
    }

    private void OnPlayerClick()
    {
        if (_boardManager.mouseOverThisTile == null) return;
        
        if (currentPlayer.selectedCardInHand == CardManager.Cards.Uninitialised) return;

        Tile selectedTile = _boardManager.mouseOverThisTile;

        if (!selectedTile.IsLegalMove()) return;
        
        _boardManager.PlaceCard(selectedTile, currentPlayer.selectedCardInHand);
    }
    
    // Called from UI
    public void SelectCardInHand(int i)
    {
        if (currentPlayer.hasPlayedACardThisTurn) return;
            
        currentPlayer.SelectCardInHand(i);

        _uiManager.ResetCardInHandColor();
        _uiManager.cardInHandSprite[i].color = currentPlayer.playerColor;
    }

    public void StartNextPlayerTurn()
    {
        currentPlayer = NextPlayerToPlay();

        currentPlayer.StartTurn(TurnNumber < 2);
        
        _uiManager.SetActiveEndTurnBtn(false);
    }

    private Player NextPlayerToPlay()
    {
        int currentIndex = Array.IndexOf(players, currentPlayer);
        int nextIndex = (currentIndex + 1) % players.Length;
        return players[nextIndex];
    }
    
    // Called from UI
    public void EndOfTurn()
    {
        if (gameIsFinish)
        {
            FindWinner();
        }
        else
        {
            _uiManager.switchTurnMenu.Show(NextPlayerToPlay());
        }
    }

    public void UpdateEachPlayerPoints()
    {
        foreach (var player in players)
        {
            player.currentPoints = 0;
        }
        
        foreach (var tile in _boardManager.tileMatrix)
        {
            if (tile.cardOnThisTile == null) continue;
            tile.cardOnThisTile.owner.currentPoints += tile.cardOnThisTile.CurrentPointsValue();
        }
        
        for (var i = 0; i < players.Length; i++)
        {
            var player = players[i];
            
            if (player.currentPoints > 100)
            {
                player.currentPoints = 100;
                gameIsFinish = true;
            }
            
            _uiManager.UpdateSliderPoints(i + 1, player.currentPoints);
        }
    }

    private void FindWinner()
    {
        if (players[0].currentPoints > players[1].currentPoints)
        {
            _uiManager.ShowWinText("Player 1 won!");
        }
        else if (players[0].currentPoints < players[1].currentPoints)
        {
            _uiManager.ShowWinText("Player 2 won!");
        }
        else
        {
            _uiManager.ShowWinText("Draw!");
        }
    }
}