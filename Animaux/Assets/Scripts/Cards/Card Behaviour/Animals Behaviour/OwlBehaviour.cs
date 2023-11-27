namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class OwlBehaviour : CardBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();

            uint scoreToAdd = 0;

            foreach (var tile in myTile.GetAllLinkedGivenCard(CardManager.Cards.Forest))
            {
                scoreToAdd += data.additionalScore;
            }
            
            AddScoreToOwner(scoreToAdd);
        }
    }
}