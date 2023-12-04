using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

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
        public CardManager.CardBiome biome;
        public CardManager.CardType type;
        [ShowIf("CheckIfAnimal")] public CardManager.CardCategory category;
        public CardManager.EffectType effectType;

        [Header("UI Info Data")] 
        public string cardFullName;
        [TextArea] public string cardEffect;
        [TextArea] public string cardDescription;

        [ShowIf("CheckIfScore"), Space] public uint pointsValue;

        [ShowIf("CheckIfNegate"), Space] public List<CardManager.CardBiome>  negatedBiomes;
        [ShowIf("CheckIfNegate")] public List<CardManager.CardType>  negatedTypes;
        [ShowIf("CheckIfNegate")] public List<CardManager.CardCategory>  negatedCategory;
        
        private bool CheckIfScore() => effectType == CardManager.EffectType.score;
        private bool CheckIfNegate() => effectType == CardManager.EffectType.negate;
        private bool CheckIfAnimal() => type == CardManager.CardType.Animal;
    }
}