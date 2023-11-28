namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class SquirrelBehaviour : CardBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();

            uint scoreToAdd = 0;
            
            // Get point for himself AND for all linked squirrels
            scoreToAdd += data.additionalScore;

            var linkedSquirrels = myTile.GetAllLinkedGivenCard(CardManager.Cards.Squirrel);
            
            for (int i = 0; i < linkedSquirrels.Count; i++)
            {
                scoreToAdd += data.additionalScore;
            }
            
            AddScoreToPlayer(owner, scoreToAdd);
        }
    }
}