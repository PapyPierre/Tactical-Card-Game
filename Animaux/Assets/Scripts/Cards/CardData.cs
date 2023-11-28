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
        
        [Header("Card Data")]
        public CardManager.Cards thisCard;
        public CardManager.CardBiomes biome;
        public CardManager.CardType type;
        public CardManager.CardCategory category;
        public CardManager.EffectType effectType;

        [Header("UI Info Data")] 
        public string cardFullName;
        [TextArea] public string cardEffect;
        [TextArea] public string cardDescription;

        [ShowIf("CheckIfScore"), Space]
        public uint additionalScore;
        
        [ShowIf("CheckIfNegate"), Space]
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