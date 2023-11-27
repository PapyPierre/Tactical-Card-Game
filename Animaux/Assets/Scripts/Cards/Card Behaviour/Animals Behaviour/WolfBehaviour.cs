namespace Cards
{
    public class WolfBehaviour : CardBehaviour
    {
        protected override void OnPose()
        {
            NegateAdjacenteTiles();
        }
    }
}