using System;
using Cards;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardInfoDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        
        [SerializeField] private TextMeshProUGUI cardNameTMP;
        [SerializeField] private TextMeshProUGUI cardInfoTMP;
        [SerializeField] private TextMeshProUGUI cardEffectTMP;
        [SerializeField] private TextMeshProUGUI cardDescriptionTMP;

        private void Start()
        {
            HideCardInfoDisplay();
        }

        public void HideCardInfoDisplay()
        {
            parent.SetActive(false);
        }
        
        public void ShowCardInfoDisplay(CardManager.Cards card)
        {
            parent.SetActive(true);
            var cardData = CardManager.instance.allCardsData[(int) card];
            string cardInfo = cardData.biome + " - " + cardData.category; 
            
            cardNameTMP.text =  cardData.cardFullName;
            cardInfoTMP.text = cardInfo;
            cardEffectTMP.text = cardData.cardEffect;
            cardDescriptionTMP.text = cardData.cardDescription;
        }
    }
}
