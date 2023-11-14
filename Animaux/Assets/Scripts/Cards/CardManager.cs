using System.Collections.Generic;

namespace Cards
{
    public class CardManager : Singleton<CardManager>
    {
        public enum Cards
        {
            Wolf,
            Owl,
            Deer,
            Squirrel,
            Boar,
            Rabbit,
            Farm,
            Forest,
            Lake,
            WaterMill,
            Uninitialised,
        }

        public enum CardBiomes
        {
            None,
            Forest,
        }
        
        public enum CardType
        {
            Animal, 
            Place,
        }

        public enum CardCategory
        {
            None,
            Predator,
            FlyingAnimal, 
            Prey,
            Building,
            NaturalPlace
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
