using System;
using Cards;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardInfoDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject cardPreview;
        
        [SerializeField] private TextMeshProUGUI cardNameTMP;
        [SerializeField] private TextMeshProUGUI cardInfoTMP;
        [SerializeField] private TextMeshProUGUI cardEffectTMP;
        [SerializeField] private TextMeshProUGUI cardDescriptionTMP;

        private void Update()
        {
            //TODO faire qu'on puisse manipuler la preview en swipant 
        }

        public void SetUpInfos(CardManager.Cards card)
        {
            cardPreview.SetActive(true);
            //TODO mettre à jour le mesh de la preview
                
            var cardData = CardManager.instance.allCardsData[(int) card];
            string cardInfo = cardData.biome + " - " + cardData.category; 
            
            cardNameTMP.text =  cardData.cardFullName;
            cardInfoTMP.text = cardInfo;
            cardEffectTMP.text = cardData.cardEffect;
            cardDescriptionTMP.text = cardData.cardDescription;
        }
    }
}
