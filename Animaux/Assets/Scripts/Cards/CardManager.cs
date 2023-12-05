using System;
using System.Collections.Generic;

namespace Cards
{
    public class CardManager : Singleton<CardManager>
    {
        [Serializable]
        public class CardParam
        {
            public CardBiome biome;
            public CardType type;
            public CardCategory category;
        }
        
        public enum Cards
        {
            Wolf = 0,
            Owl = 1,
            Deer = 2,
            Squirrel = 3,
            Boar = 4,
            Farm = 5,
            Forest = 6,
            Lake = 7,
            WaterMill = 8,
            None = 9,
        }

        [Flags]
        public enum CardBiome
        {
            Forest = 1 << 0,
            Aquatic = 1 << 1,
            Artificial = 1 << 2,
        }
        
        [Flags]
        public enum CardType
        {
            Animal = 1 << 0, 
            Location = 1 << 1,
        }

        [Flags]
        public enum CardCategory
        {
            Carnivorous = 1 << 0,
            Flying = 1 << 1, 
            Herbivorous = 1 << 2,
            Omnivorous  = 1 << 3,
        }

        public enum EffectType
        {
            score,
            negate,
            other
        }

        public List<CardData> allCardsData;
    }
}
