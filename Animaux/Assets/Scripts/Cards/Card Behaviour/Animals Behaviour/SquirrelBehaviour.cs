namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class SquirrelBehaviour : AnimalBehaviour
    {
        public override uint CurrentPointsValue()
        {
            uint pointsValue = 0;

            // Get point for himself AND for all linked squirrels
            pointsValue += data.pointsValue;

            int linkedSquirrelsCount = myTile.GetAllLinkedGivenCard(CardManager.Cards.Squirrel).Count;

            for (int i = 0; i < linkedSquirrelsCount; i++)
            {
                pointsValue += data.pointsValue;
            }

            currentPointsValue = pointsValue;

            return base.CurrentPointsValue();
        }
    }
}