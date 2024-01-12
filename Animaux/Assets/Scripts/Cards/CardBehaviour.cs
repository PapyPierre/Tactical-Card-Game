using System.Collections.Generic;
using Board;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    public class CardBehaviour : MonoBehaviour
    {
        [Expandable] public CardData data;
        [HideInInspector] public readonly List<CardBehaviour> isNegateBy = new();
        [HideInInspector] public Tile myTile;
        internal Player owner;
        protected uint currentPointsValue;
        [SerializeField] private List<MeshRenderer> _meshRenderersToColor;

        public void Init(Tile posedOnTile, Player cardOwner)
        {
            myTile = posedOnTile;
            owner = cardOwner;
            OnPose();
        }

        protected virtual void OnPose()
        {
            myTile.cardOnThisTile = this;
            
            myTile.SetMat(BoardManager.instance.baseTileMat);
            
            foreach (var tile in myTile.AdjacentTile())
            {
                if (tile.cardOnThisTile == null) continue;
              
                if (tile.cardOnThisTile.data.effectType != CardManager.EffectType.negate) continue;
                
                foreach (var cardParam in tile.cardOnThisTile.data.negatedCardParam)
                {
                    if (myTile.Is(cardParam.biome, cardParam.type, cardParam.category))
                    {
                        Negate(tile.cardOnThisTile);
                    }
                }
            }
            
            GameManager.instance.UpdateEachPlayerPoints();

            foreach (var mr in _meshRenderersToColor)
            {
                mr.material = owner.playerMat;
            }
        }

        public virtual uint CurrentPointsValue()
        {
            if (isNegateBy.Count > 0) currentPointsValue = 0;
            //Debug.Log($"{data.thisCard} at {transform.position} points value = {currentPointsValue}");
            return currentPointsValue;
        }

        protected void NegateAdjacentCards()
        {
            if (data.effectType != CardManager.EffectType.negate)
            {
                Debug.LogError("Function shouldn't have been called, card is does not have a negate type effet");
                return;
            }

            List<Tile> adjacentTiles = myTile.AdjacentTile();

            foreach (var tile in adjacentTiles)
            {
                if (tile.cardOnThisTile == null)
                {
                    tile.SetMat(BoardManager.instance.effectOnTileMat);
                    tile.baseSR.sprite = data.sprite;
                }
            }
            
            foreach (var cardParam in data.negatedCardParam)
            {
                foreach (var tile in adjacentTiles.WhichIs(cardParam.biome, cardParam.type, cardParam.category)) 
                { 
                    tile.cardOnThisTile.Negate(this);
                }
            }
        }

        protected void Negate(CardBehaviour cardIsNegatesBy)
        {
            Debug.Log(myTile.cardOnThisTile +" is negated !");

            
            if (!isNegateBy.Contains(cardIsNegatesBy))
            {
                isNegateBy.Add(cardIsNegatesBy);
            }
            
            myTile.SetMat(BoardManager.instance.negatedTileMat);
        }

        public bool IsNegate()
        {
            return isNegateBy.Count > 0;
        }
    }
}