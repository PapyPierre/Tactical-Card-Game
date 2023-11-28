namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class WaterMillBehaviour : LocationBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();

            uint scoreToAdd = 0;

            int aquaticAdjacentTilesCount = myTile.AdjacentTile().WhichIs(CardManager.CardBiomes.Aquatic).Count;
            
            for (var index = 0; index < aquaticAdjacentTilesCount; index++)
            {
                scoreToAdd += data.additionalScore;
            }

            AddScoreToPlayer(owner, scoreToAdd);
        }
    }
}