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

        public void Init(Tile posedOnTile, Player cardOwner)
        {
            myTile = posedOnTile;
            owner = cardOwner;
            OnPose();
        }

        protected virtual void OnPose()
        {
            
        }
        
        public virtual void OnScoreCompute()
        {
            if (isNegateBy.Count > 0) return;
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

        protected void AddScoreToPlayer(Player player, uint score)
        {
            player.numberOfPoints += score;
            //TODO zoli vfx de tess
        }
    }
}
