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

        public enum CardBiome
        {
            None = 0,
            Forest = 1,
            Aquatic = 2,
            Artificial = 3,
        }
        
        public enum CardType
        {
            None,
            Animal, 
            Location,
        }

        public enum CardCategory
        {
            None,
            Carnivorous,
            Flying, 
            Herbivorous,
            Omnivorous,
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
