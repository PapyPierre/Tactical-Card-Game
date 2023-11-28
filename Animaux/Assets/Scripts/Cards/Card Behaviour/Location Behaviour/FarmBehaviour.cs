namespace Cards.Card_Behaviour.Location_Behaviour
{
    public class FarmBehaviour : CardBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();
            AddScoreToPlayer(owner, data.additionalScore);
        }
    }
}