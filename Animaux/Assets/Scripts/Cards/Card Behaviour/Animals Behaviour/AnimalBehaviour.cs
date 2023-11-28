namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class AnimalBehaviour : CardBehaviour
    {
        protected override void OnPose()
        {
            base.OnPose();
            
            if (data.category == CardManager.CardCategory.Herbivorous)
            {
                foreach (var tile in myTile.AdjacentTile().WhichIs(CardManager.Cards.Wolf))
                {
                    Negate(tile.cardOnThisTile);
                }
            }
        }
    }
}