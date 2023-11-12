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

        public List<CardData> allCardsData;
    }
}
