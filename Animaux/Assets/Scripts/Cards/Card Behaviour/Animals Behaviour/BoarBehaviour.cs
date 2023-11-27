namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class BoardBehaviour : CardBehaviour
    {
        protected override void OnPose()
        {
            NegateAdjacenteTiles();
        }
    }
}