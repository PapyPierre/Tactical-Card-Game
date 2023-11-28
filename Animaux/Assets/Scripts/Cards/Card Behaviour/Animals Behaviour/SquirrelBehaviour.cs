namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class SquirrelBehaviour : AnimalBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();

            uint scoreToAdd = 0;
            
            // Get point for himself AND for all linked squirrels
            scoreToAdd += data.additionalScore;

            int linkedSquirrelsCount = myTile.GetAllLinkedGivenCard(CardManager.Cards.Squirrel).Count;
            
            for (int i = 0; i < linkedSquirrelsCount; i++)
            {
                scoreToAdd += data.additionalScore;
            }
            
            AddScoreToPlayer(owner, scoreToAdd);
        }
    }
}