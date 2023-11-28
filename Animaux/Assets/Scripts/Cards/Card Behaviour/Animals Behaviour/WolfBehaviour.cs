namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class WolfBehaviour : AnimalBehaviour
    {
        protected override void OnPose()
        {           
            base.OnPose();

            NegateAdjacentCards();
        }
    }
}