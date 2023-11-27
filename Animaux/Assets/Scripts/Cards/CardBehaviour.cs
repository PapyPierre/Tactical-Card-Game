using System.Collections.Generic;
using Board;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cards
{
    public class CardBehaviour : MonoBehaviour
    {
        public CardData data;
        [HideInInspector] public List<CardBehaviour> isNegateBy = new ();
        [HideInInspector] public Tile myTile;
        protected Player owner;

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
        
        protected void NegateAdjacenteTiles()
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
                    tile.cardOnThisTile.isNegateBy.Add(this);
                }
            }
        }

        protected void AddScoreToOwner(uint score)
        {
            owner.numberOfPoints += score;
            //TODO zoli vfx de tess
        }
    }
}
