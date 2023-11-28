﻿namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class LocationBehaviour : CardBehaviour
    {
        protected override void OnPose()
        {
            base.OnPose();

            foreach (var tile in myTile.AdjacentTile().WhichIs(CardManager.Cards.Boar))
            {
                Negate(tile.cardOnThisTile);
            }
        }
    }
}