using System.Collections.Generic;

namespace Cards
{
    public class CardManager : Singleton<CardManager>
    {
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
            Uninitialised = 9,
        }

        public enum CardBiomes
        {
            None,
            Forest,
            Aquatic,
            Artificial,
        }
        
        public enum CardType
        {
            Animal, 
            Location,
        }

        public enum CardCategory
        {
            None,
            Carnivorous,
            Flying, 
            Herbivorous,
            Building,
            Scenery,
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
