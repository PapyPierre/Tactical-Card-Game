namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class DeerBehaviour : AnimalBehaviour
    {
        protected override void OnPose()
        {
            base.OnPose();

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

            int adjacentForestCount = myTile.AdjacentTile().WhichIs(CardManager.Cards.Forest).Count;
            
            for (var index = 0; index < adjacentForestCount; index++)
            {
                adjacentForestIndex++;
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