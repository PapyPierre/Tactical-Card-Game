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

        [SerializeField] private GameObject tilePrefab;
        public Transform tilesParent;
        public Transform playedCardsParent;

        private const int boardSize = 6;
        public readonly Tile[,] tileMatrix = new Tile[boardSize, boardSize];

        [HideInInspector] public Tile lastPlayedOnTile;
        [SerializeField] private Material baseTileMat;
        public Material effectOnTileMat;
        public Material negatedTileMat;

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
                    Tile newTile = Instantiate(tilePrefab, tilePos, Quaternion.identity, tilesParent).GetComponent<Tile>();
                    tileMatrix[(int)tilePos.x, z] = newTile;
                }
            }
        }

        public void PlaceCard(Tile tile, CardManager.Cards card)
        {
            Player currentPlayer = _gameManager.currentPlayer;

            currentPlayer.hasPlayedACardThisTurn = true;
            
            currentPlayer.cardsInHand.Remove(currentPlayer.selectedCardInHand);
            _uiManager.UpdateCardInHandSprites();
            _uiManager.ResetCardInHandColor();
            currentPlayer.selectedCardInHand = CardManager.Cards.None;
            _uiManager.ResetCardInHandScale();

            Vector3 tilePos = tile.transform.position;
            
            //BUG THERE WAS A BUG HERE
            Debug.Log((int) card);
            CardData cardData = CardManager.instance.allCardsData[(int) card];
            
            Vector3 pos = new Vector3(tilePos.x, 0.1f, tilePos.z);
            
            var posedCard = Instantiate(cardData.prefab, pos, cardData.prefab.transform.rotation, playedCardsParent).GetComponent<CardBehaviour>(); 
            posedCard.Init(tile, currentPlayer);
            
            // For Debug, waiting for 3D models
            if (cardData.sprite)
            {
                tile.baseSR.sprite = cardData.sprite;
                // tile.baseSR.color = currentPlayer.playerColor;
            }
            
            tile.cardOnThisTile = posedCard;

           // if (lastPlayedOnTile != null) lastPlayedOnTile.SetMat(lastPlayedOnTile.cardOnThisTile.owner.playerMat);
            lastPlayedOnTile = tile;
            if (!lastPlayedOnTile.cardOnThisTile.IsNegate())
            {
                lastPlayedOnTile.SetMat(currentPlayer.playerMat);
            }
            
            _uiManager.SetActiveCancelTurnBtn(true);
            _uiManager.SetActiveEndTurnBtn(true);
            CheckIfBoardIsFull();
        }

        private void CheckIfBoardIsFull()
        {
            if (GetOccuppiedTile().Count >= boardSize * boardSize)
            {
                _gameManager.gameIsFinish = true;
            }
        }

        private List<Tile> GetOccuppiedTile()
        {
            List<Tile> occupiedTiles = new List<Tile>();
            
            foreach (var tile in tileMatrix)
            {
                if (tile.cardOnThisTile)
                {
                    occupiedTiles.Add(tile);
                }
            }

            return occupiedTiles;
        }

        public List<Tile> GetLegalMoves()
        {
            List<Tile> moves = new List<Tile>();

            foreach (var tile in tileMatrix)
            {
                if (tile.IsLegalMove())
                {
                    moves.Add(tile);
                }
            }

            return moves;
        }

        public void ResetTile(Tile tile)
        {
            tile.SetMat(tile.cardOnThisTile.IsNegate() ? effectOnTileMat : baseTileMat);

            if (tile.cardOnThisTile.data.effectType == CardManager.EffectType.negate)
            {
                foreach (var adjacenteTile in tile.AdjacentTile())
                {
                    if (adjacenteTile.cardOnThisTile == null) continue;
               
                    if (adjacenteTile.cardOnThisTile.isNegateBy.Contains(tile.cardOnThisTile))
                    {
                        adjacenteTile.cardOnThisTile.isNegateBy.Remove(tile.cardOnThisTile);
                    }
                }
                
                _gameManager.UpdateEachPlayerPoints();
                
                UpdateTilesMat(tile.AdjacentTile());
            }
            
            tile.baseSR.sprite = null;
            
            Destroy(tile.cardOnThisTile.gameObject);

            tile.cardOnThisTile = null;
        }

        private void UpdateTilesMat(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                if (tile.cardOnThisTile != null)
                {
                    if (tile.cardOnThisTile.IsNegate())
                    {
                        tile.SetMat(negatedTileMat);
                        continue;
                    }
                    else
                    {
                        tile.SetMat(tile.cardOnThisTile.owner.playerMat);
                        continue;
                    }
                }
                else
                {
                    foreach (var adjacenteTile in tile.AdjacentTile())
                    {
                        if (adjacenteTile.cardOnThisTile == null) continue;
                        
                        if (adjacenteTile.cardOnThisTile.data.effectType == CardManager.EffectType.negate)
                        {
                            tile.SetMat(effectOnTileMat);
                        }
                    }
                }
                
                tile.SetMat(baseTileMat);
            }
        }
    }
}