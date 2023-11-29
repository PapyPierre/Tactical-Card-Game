namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class OwlBehaviour : AnimalBehaviour
    {
        public override uint CurrentPointsValue()
        {
            uint pointsValue = 0;

            int linkedForestsCount = myTile.GetAllLinkedGivenCard(CardManager.Cards.Forest).Count;
            for (var index = 0; index < linkedForestsCount; index++)
            {
                pointsValue += data.pointsValue;
            }

            currentPointsValue = pointsValue;
            
            return base.CurrentPointsValue();
        }
    }
}