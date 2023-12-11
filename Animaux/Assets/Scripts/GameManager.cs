using System;
using Board;
using Cards;
using UI;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private BoardManager _boardManager;
    private UIManager _uiManager;

    private bool gameHasStarted;
    [HideInInspector] public bool gameIsFinish;

    private int turnNumber;

    public int TurnNumber
    {
        get => turnNumber;

        set
        {
            turnNumber = value;
            _uiManager.turnNumberTMP.text = value.ToString();
        }
    }

    [SerializeField] private uint pointRequiredToWin;
    
    public Player[] players;
    [HideInInspector] public Player currentPlayer;

    private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    private void Start()
    {
        Application.targetFrameRate = 60;

        _boardManager = BoardManager.instance;
        _uiManager = UIManager.instance;

        _camera = Camera.main;
    }

    public void StartGame()
    {
        _boardManager.Init();
        currentPlayer = players[0];
        currentPlayer.StartTurn(true);
        gameHasStarted = true;
        gameIsFinish = false;
    }

    public void CheckToPlayCard(Vector2 touchPosOnScreen)
    {
        if (!gameHasStarted) return;
        if (currentPlayer.hasPlayedACardThisTurn) return;

        Ray ray = _camera.ScreenPointToRay(touchPosOnScreen);
        
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2);

        if (Physics.Raycast(ray, out var hit, 100, _layerMask))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                Tile selectedTile = hit.collider.GetComponent<Tile>();
                if (!selectedTile.IsLegalMove()) return;
                _boardManager.PlaceCard(selectedTile, currentPlayer.selectedCardInHand);
            }
        }
    }

    public void StartNextPlayerTurn()
    {
        currentPlayer = NextPlayerToPlay();

        currentPlayer.StartTurn(TurnNumber < 2);
        
        _uiManager.SetActiveEndTurnBtn(false);
        
        _uiManager.ResetCardInHandScale();
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
            foreach (var cardInHand in _uiManager.cardsInHandDisplays)
            {
                cardInHand.UnSelect();
            }
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

            if (player.currentPoints > pointRequiredToWin)
            {
                player.currentPoints = pointRequiredToWin;
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