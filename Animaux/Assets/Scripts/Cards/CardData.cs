using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 0)]
    public class CardData : ScriptableObject
    {
        [Header("Meta Data")]
        public GameObject prefab;
        public Sprite sprite;
        public bool hasSpecialEffectOnPose;
        
        [Header("Card Data")]
        public CardManager.Cards thisCard;
        public CardManager.CardBiomes biome;
        public CardManager.CardType type;
        public CardManager.CardCategory category;
        public CardManager.EffectType effectType;
        
        [ShowIf("CheckIfScore")]
        public int additionalScore;
        
        [ShowIf("CheckIfNegate")]
        public List<CardManager.CardCategory>  negatedCategory;

        private bool CheckIfScore()
        {
            return effectType == CardManager.EffectType.score;
        }
        
        private bool CheckIfNegate()
        {
            return effectType == CardManager.EffectType.negate;
        }
    }
}