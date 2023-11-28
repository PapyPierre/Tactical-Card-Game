namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class OwlBehaviour : AnimalBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();

            uint scoreToAdd = 0;

            int linkedForestsCount = myTile.GetAllLinkedGivenCard(CardManager.Cards.Forest).Count;
            
            for (var index = 0; index < linkedForestsCount; index++)
            {
                scoreToAdd += data.additionalScore;
            }

            AddScoreToPlayer(owner, scoreToAdd);
        }
    }
}