namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class DeerBehaviour : CardBehaviour
    {
        protected override void OnPose()
        {
            CheckForWin();
        }

        public void CheckForWin()
        {
            if (IsSurroundedByForest())
            {
                InstantWin();
            }
        }
        
        private bool IsSurroundedByForest()
        {
            int adjacentForestIndex = 0;

            foreach (var tile in myTile.AdjacentTile())
            {
                if (!tile.cardOnThisTile) continue;

                if (tile.cardOnThisTile.data.thisCard == CardManager.Cards.Forest)
                {
                    adjacentForestIndex++;
                }
            }
            
            return adjacentForestIndex == 4;
        }

        private void InstantWin()
        {
            AddScoreToPlayer(owner, 9999);
            GameManager.instance.gameIsFinish = true;
        }
    }
}