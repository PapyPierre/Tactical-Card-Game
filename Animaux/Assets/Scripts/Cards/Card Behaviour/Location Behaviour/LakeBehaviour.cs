namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class LakeBehaviour : LocationBehaviour
    {
        public override uint CurrentPointsValue()
        {
            uint pointsValue = 0;

            var adjacentAnimal = myTile.AdjacentTile().WhichIs(CardManager.CardType.Animal);
            
            for (var index = 0; index < adjacentAnimal.Count; index++)
            {
                pointsValue += data.pointsValue;
            }

            currentPointsValue = pointsValue;
            return base.CurrentPointsValue();
        }
    }
}