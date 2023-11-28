﻿namespace Cards.Card_Behaviour.Animals_Behaviour
{
    public class OwlBehaviour : CardBehaviour
    {
        public override void OnScoreCompute()
        {
            base.OnScoreCompute();

            uint scoreToAdd = 0;

            var linkedForests = myTile.GetAllLinkedGivenCard(CardManager.Cards.Forest);
            
            for (var index = 0; index < linkedForests.Count; index++)
            {
                scoreToAdd += data.additionalScore;
            }

            AddScoreToPlayer(owner, scoreToAdd);
        }
    }
}