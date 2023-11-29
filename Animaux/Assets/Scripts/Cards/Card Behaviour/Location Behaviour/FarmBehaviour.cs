namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class FarmBehaviour : LocationBehaviour
    {
        public override uint CurrentPointsValue()
        {
            currentPointsValue = data.pointsValue;
            return base.CurrentPointsValue();
        }
    }
}