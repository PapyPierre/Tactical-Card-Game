using System;
using Board;
using Cards;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private BoardManager _boardManager;

    private bool gameHasStarted;
    [HideInInspector] public int turnNumber;
    [HideInInspector] public bool gameIsFinish;

    public Player[] players;
    [HideInInspector] public Player currentPlayingPlayer;
    
    private void Start()
    {
        _boardManager = BoardManager.instance;
    }
    
    public void StartGame()
    {
        _boardManager.Init();
        currentPlayingPlayer = players[0];
        currentPlayingPlayer.StartTurn(true);
        gameHasStarted = true;
    }
    
    private void Update()
    {
        if (!gameHasStarted) return;
       
        if (Input.GetMouseButtonDown(0) && currentPlayingPlayer.isReadyToPlay)
        {
            OnPlayerClick();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            gameIsFinish = true;
        }
    }

    private void OnPlayerClick()
    {
        if (_boardManager.mouseOverThisTile == null) return;
        
        if (currentPlayingPlayer.selectedCardInHand == CardManager.Cards.Uninitialised) return;

        Tile selectedTile = _boardManager.mouseOverThisTile;

        if (!selectedTile.IsLegalMove()) return;
        
        _boardManager.PlaceCard(selectedTile, currentPlayingPlayer.selectedCardInHand);
    }

    public void StartNextPlayerTurn()
    {
        currentPlayingPlayer = NextPlayerToPlay();

        currentPlayingPlayer.StartTurn(turnNumber < 2);
    }

    public Player NextPlayerToPlay()
    {
        
        int currentIndex = Array.IndexOf(players, currentPlayingPlayer);
        int nextIndex = (currentIndex + 1) % players.Length;
        return players[nextIndex];
    }

    public void ComputePoints()
    {
        foreach (var tile in _boardManager.tileMatrix)
        {
            tile.cardOnThisTile.OnScoreCompute();
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