using System.Collections.Generic;
using Cards;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardInfoDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject cardPreview;
        [SerializeField] private List<GameObject> allCardsPreviews;   
        [SerializeField] private TextMeshProUGUI cardNameTMP;
        [SerializeField] private TextMeshProUGUI cardBiomeTMP;
        [SerializeField] private TextMeshProUGUI cardCategoryTMP;
        [SerializeField] private TextMeshProUGUI cardEffectTMP;
        [SerializeField] private TextMeshProUGUI cardDescriptionTMP;

        public void SetUpInfos(CardManager.Cards card)
        {
            cardPreview.SetActive(true);
            allCardsPreviews[(int) card].SetActive(true);
                
            var cardData = CardManager.instance.allCardsData[(int) card];
            
            cardNameTMP.text =  cardData.cardFullName;
            cardBiomeTMP.text = cardData.biome.ToString();

            cardCategoryTMP.text = cardData.type is CardManager.CardType.Animal ? cardData.category.ToString() : cardData.type.ToString();
            
            cardEffectTMP.text = cardData.cardEffect;
         //   cardDescriptionTMP.text = cardData.cardDescription;
        }

        private void OnDisable()
        {
            foreach (var go in allCardsPreviews)
            {
                if (go.activeSelf)
                {
                    go.SetActive(false);
                }
            }
            
            cardPreview.SetActive(false);
        }
    }
}
