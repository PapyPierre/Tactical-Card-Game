namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class DeerBehaviour : AnimalBehaviour
    {
        public override uint CurrentPointsValue()
        {
            CheckForWin();
            return base.CurrentPointsValue();
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
            currentPointsValue = 100;
            GameManager.instance.gameIsFinish = true;
        }
    }
}