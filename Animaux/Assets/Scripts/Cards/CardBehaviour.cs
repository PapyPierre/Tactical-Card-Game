using System.Collections.Generic;
using Board;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    public class CardBehaviour : MonoBehaviour
    {
        [Expandable] public CardData data;
        private readonly List<CardBehaviour> isNegateBy = new();
        [HideInInspector] public Tile myTile;
        internal Player owner;
        protected uint currentPointsValue;

        public void Init(Tile posedOnTile, Player cardOwner)
        {
            myTile = posedOnTile;
            owner = cardOwner;
            OnPose();
        }

        protected virtual void OnPose()
        {
            myTile.cardOnThisTile = this;
            GameManager.instance.UpdateEachPlayerPoints();
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
            
            foreach (var biome in data.negatedBiomes)
            {
                foreach (var tile in adjacentTiles.WhichIs(biome))
                {
                    tile.cardOnThisTile.Negate(this);
                }
            }

            foreach (var type in data.negatedTypes)
            {
                foreach (var tile in adjacentTiles.WhichIs(type))
                {
                    tile.cardOnThisTile.Negate(this);
                }
            }

            foreach (var category in data.negatedCategory)
            {
                foreach (var tile in adjacentTiles.WhichIs(category))
                {
                    tile.cardOnThisTile.Negate(this);
                }
            }
        }

        protected void Negate(CardBehaviour cardIsNegatesBy)
        {
            isNegateBy.Add(cardIsNegatesBy);
            myTile.overlapSR.enabled = true;
        }
    }
}