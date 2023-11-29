using System.Collections.Generic;
using Cards;
using UI;
using UnityEngine;

namespace Board
{
    public class BoardManager : Singleton<BoardManager>
    {
        private GameManager _gameManager;
        private UIManager _uiManager;
        
        [HideInInspector] public Tile mouseOverThisTile;

        [SerializeField] private GameObject tilePrefab;
        
        public const int boardSize = 7;
        public readonly Tile[,] tileMatrix = new Tile[boardSize, boardSize];
    
        private readonly List<Tile> legalMoves = new ();
        private bool lastTurnWasSkipped;

        private void Start()
        {
            _gameManager = GameManager.instance;
            _uiManager = UIManager.instance;
        }

        public void Init()
        {
            GenerateBoard();
        }

        private void GenerateBoard()
        {
            Vector3 tilePos;

            for (int x = 0; x < boardSize; x++)
            {
                tilePos = new Vector3(x, 0, 0);

                for (int z = 0; z < boardSize; z++)
                {
                    tilePos = new Vector3(tilePos.x, tilePos.y, z);
                    Tile newTile = Instantiate(tilePrefab, tilePos, Quaternion.identity).GetComponent<Tile>();
                    tileMatrix[(int)tilePos.x, z] = newTile;
                }
            }
        }

        public void PlaceCard(Tile tile, CardManager.Cards card)
        {
            _uiManager.cardInfoDisplayer.HideCardInfoDisplay();
            
            Player currentPlayer =  _gameManager.currentPlayer;

            currentPlayer.hasPlayedACardThisTurn = true;
            
            currentPlayer.cardsInHand.Remove(currentPlayer.selectedCardInHand);
            _uiManager.UpdateCardInHandSprites();
            _uiManager.ResetCardInHandColor();
            currentPlayer.selectedCardInHand = CardManager.Cards.Uninitialised;
            
            Vector3 tilePos = tile.transform.position;
            CardData cardData = CardManager.instance.allCardsData[(int) card];
            
            Vector3 pos = new Vector3(tilePos.x, 0.1f, tilePos.z);
            
            var posedCard = Instantiate(cardData.prefab, pos, Quaternion.identity).GetComponent<CardBehaviour>(); 
            posedCard.Init(tile, currentPlayer);
            
            // For Debug, waiting for 3D models
            if (cardData.sprite)
            {
                tile.baseSR.sprite = cardData.sprite;
                tile.baseSR.color = currentPlayer.playerColor;
            }
            
            Vector2Int tileCoord = new Vector2Int((int) tilePos.x, (int) tilePos.z);
            tileMatrix[tileCoord.x, tileCoord.y].cardOnThisTile = posedCard;
            _uiManager.SetActiveEndTurnBtn(true);
            CheckIfBoardIsFull();
        }

        private void CheckIfBoardIsFull()
        {
            uint occupiedTile = 0; 
            
            foreach (var tile in tileMatrix)
            {
                if (tile.cardOnThisTile)
                {
                    occupiedTile++;
                }
            }

            if (occupiedTile >= boardSize * boardSize)
            {
                _gameManager.gameIsFinish = true;
            }
        }
    }
}