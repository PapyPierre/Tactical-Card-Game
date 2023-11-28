using Cards.Card_Behaviour.Animals_Behaviour;

namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class ForestBehaviour : CardBehaviour
    {
        protected override void OnPose()
        {
            foreach (var tile in myTile.AdjacentTile())
            {
                if (!tile.cardOnThisTile) continue;
                
                if (tile.cardOnThisTile.data.thisCard == CardManager.Cards.Deer)
                {
                    tile.cardOnThisTile.GetComponent<DeerBehaviour>().CheckForWin();
                }
            }
        }
        
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
            
            AddScoreToPlayer(owner, scoreToAdd);
        }
    }
}