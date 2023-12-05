﻿namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class AnimalBehaviour : CardBehaviour
    {
        protected override void OnPose()
        {
            if (data.category == CardManager.CardCategory.Herbivorous)
            {
                //TODO Rendre modulaire ca
                foreach (var tile in myTile.AdjacentTile().WhichIs(CardManager.Cards.Wolf))
                {
                    Negate(tile.cardOnThisTile);
                }
            }
            
            base.OnPose();
        }
    }
}