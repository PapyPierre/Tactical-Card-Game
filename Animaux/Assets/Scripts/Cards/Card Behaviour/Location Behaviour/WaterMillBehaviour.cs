namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class WaterMillBehaviour : CardBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();

            uint scoreToAdd = 0;
            
            foreach (var tile in myTile.AdjacentTile())
            {
                if (!tile.cardOnThisTile) continue;
                
                if (tile.cardOnThisTile.data.thisCard == CardManager.Cards.Lake)
                {
                    scoreToAdd += data.additionalScore;
                }
            }   
            
            AddScoreToOwner(scoreToAdd);
        }
    }
}