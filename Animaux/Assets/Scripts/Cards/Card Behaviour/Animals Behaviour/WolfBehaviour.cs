namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class WolfBehaviour : AnimalBehaviour
    {
        protected override void OnPose()
        {           
            NegateAdjacentCards();
            base.OnPose();
        }
    }
}