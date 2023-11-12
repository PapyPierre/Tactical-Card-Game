using System;
using Board;
using Cards;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private BoardManager _boardManager;

    private bool gameHasStarted;
    [HideInInspector] public int turnNumber;

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
    }

    private void OnPlayerClick()
    {
        if (_boardManager.mouseOverThisTile == null) return;
        
        if (currentPlayingPlayer.selectedCardInHand == CardManager.Cards.Uninitialised) return;

        Tile selectedTile = _boardManager.mouseOverThisTile;

        if (!selectedTile.IsLegalMove()) return;
        
        _boardManager.PlaceCard(selectedTile, currentPlayingPlayer.selectedCardInHand);
    }

    public void EndOfTurn()
    {
        StartNextPlayerTurn();
    }

    private void StartNextPlayerTurn()
    {
        int currentIndex = Array.IndexOf(players, currentPlayingPlayer);
        int nextIndex = (currentIndex + 1) % players.Length;
        currentPlayingPlayer = players[nextIndex];

        currentPlayingPlayer.StartTurn(turnNumber < 2);
    }
}