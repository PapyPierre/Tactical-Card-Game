namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class WaterMillBehaviour : LocationBehaviour
    {
        public override uint CurrentPointsValue()
        {
            uint pointsValue = 0;

            int aquaticAdjacentTilesCount = myTile.AdjacentTile().WhichIs(CardManager.CardBiome.Aquatic).Count;

            for (var index = 0; index < aquaticAdjacentTilesCount; index++)
            {
                pointsValue += data.pointsValue;
            }

            currentPointsValue = pointsValue;

            return base.CurrentPointsValue();
        }
    }
}