using Cards.Card_Behaviour.Animals_Behaviour;

namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class ForestBehaviour : LocationBehaviour
    {
        protected override void OnPose()
        {
            foreach (var tile in myTile.AdjacentTile().WhichIs(CardManager.Cards.Deer))
            {
                tile.cardOnThisTile.GetComponent<DeerBehaviour>().CheckForWin();
            }
            
            base.OnPose();
        }

        public override uint CurrentPointsValue()
        {
            uint pointsValue = 0;

            foreach (var tile in myTile.AdjacentTile().WhichIs(CardManager.CardBiomes.Forest))
            {
                // Si c'est aussi un animal
                if (tile.cardOnThisTile.data.type == CardManager.CardType.Animal)
                {
                    pointsValue += data.pointsValue;
                }
            }

            currentPointsValue = pointsValue;
            return base.CurrentPointsValue();
        }
    }
}