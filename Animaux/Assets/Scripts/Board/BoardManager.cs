using System;
using System.Collections.Generic;
using Cards;
using UI;
using UnityEngine;

namespace Board
{
    public class BoardManager : Singleton<BoardManager>
    {
        private GameManager _gameManager;
        
        [HideInInspector] public Tile mouseOverThisTile;

        [SerializeField] private GameObject tilePrefab;

        private const int boardSize = 7;
        public readonly Tile[,] tileMatrix= new Tile[boardSize, boardSize];
    
        private readonly List<Tile> legalMoves = new ();
        private bool lastTurnWasSkipped;

        private void Start()
        {
            _gameManager = GameManager.instance;
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
            Player currentPlayer =  _gameManager.currentPlayingPlayer;
            
            currentPlayer.cardsInHand.Remove(currentPlayer.selectedCardInHand);
            UIManager.instance.ResetCardInHandColor();
            currentPlayer.selectedCardInHand = CardManager.Cards.Uninitialised;
            
            Vector3 tilePos = tile.transform.position;
            CardData cardData = CardManager.instance.allCardsData[(int) card];

            if (cardData.prefab != null)
            {
                Instantiate(cardData.prefab, new Vector3(tilePos.x, 0.1f, tilePos.y), Quaternion.identity);
            }

            // For Debug, waiting for 3D models
            if (cardData.sprite != null)
            {
                tile.spriteRenderer.sprite = cardData.sprite;
                tile.spriteRenderer.color = currentPlayer.playerColor;
            }
            
            Vector2Int tileCoord = new Vector2Int((int) tilePos.x, (int) tilePos.z);
            tileMatrix[tileCoord.x, tileCoord.y].cardDataOnThisTile = cardData;

            if (!cardData.hasSpecialEffectOnPose)
            {
                currentPlayer.FinishTurn();
            }
        }
    }
}