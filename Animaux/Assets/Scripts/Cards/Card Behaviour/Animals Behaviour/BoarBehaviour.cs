namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class BoarBehaviour : AnimalBehaviour
    {
        protected override void OnPose()
        {
            NegateAdjacentCards();
            base.OnPose();
        }
    }
}