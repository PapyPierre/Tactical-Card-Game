using System.Collections.Generic;
using Board;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    public class CardBehaviour : MonoBehaviour
    {
        [Expandable] public CardData data;
        private readonly List<CardBehaviour> isNegateBy = new ();
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
            
            foreach (var tile in myTile.AdjacentTile())
            {
                if (!tile.cardOnThisTile) continue;
                
                if (data.negatedCategory.Contains(tile.cardOnThisTile.data.category))
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
