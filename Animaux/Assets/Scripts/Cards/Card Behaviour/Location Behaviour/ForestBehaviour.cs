namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class ForestBehaviour : CardBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();

            uint scoreToAdd = 0;
            
            foreach (var tile in myTile.AdjacentTile())
            {
                if (!tile.cardOnThisTile) continue;
                
                if (tile.cardOnThisTile.data.biome == CardManager.CardBiomes.Forest 
                    && tile.cardOnThisTile.data.type == CardManager.CardType.Animal)
                {
                    scoreToAdd += data.additionalScore;
                }
            }   
            
            AddScoreToOwner(scoreToAdd);
        }
    }
}