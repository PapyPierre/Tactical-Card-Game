namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class WolfBehaviour : CardBehaviour
    {
        protected override void OnPose()
        {
            NegateAdjacentTiles();
        }
    }
}