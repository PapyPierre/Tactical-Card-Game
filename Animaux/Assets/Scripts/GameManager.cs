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
    [FormerlySerializedAs("currentPlayingPlayer")] [HideInInspector] public Player currentPlayer;
    
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
    public void PlayCardInHand(int i)
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
            ComputePoints();
        }
        else
        {
            _uiManager.switchTurnMenu.Show(NextPlayerToPlay());
        }
    }

    private void ComputePoints()
    {
        Debug.Log(0);
        
        foreach (var tile in _boardManager.tileMatrix)
        {
            if (tile.cardOnThisTile)
            {
                tile.cardOnThisTile.OnScoreCompute();
            }
        }

        if (players[0].numberOfPoints > players[1].numberOfPoints)
        {
            Debug.Log(players[0] + " win!");
        }
        else if (players[0].numberOfPoints < players[1].numberOfPoints)
        {
            Debug.Log(players[1] + " win!");
        }
        else
        {
            Debug.Log("Draw");
        }
    }
}