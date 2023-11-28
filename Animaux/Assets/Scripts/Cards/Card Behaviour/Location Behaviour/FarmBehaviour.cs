namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class FarmBehaviour : LocationBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();
            AddScoreToPlayer(owner, data.additionalScore);
        }
    }
}