using Cards;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardInfoDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject cardPreview;
        
        [SerializeField] private TextMeshProUGUI cardNameTMP;
        [SerializeField] private TextMeshProUGUI cardBiomeTMP;
        [SerializeField] private TextMeshProUGUI cardCategoryTMP;
        [SerializeField] private TextMeshProUGUI cardEffectTMP;
        [SerializeField] private TextMeshProUGUI cardDescriptionTMP;

        public void SetUpInfos(CardManager.Cards card)
        {
            cardPreview.SetActive(true);
            //TODO mettre à jour le mesh de la preview
                
            var cardData = CardManager.instance.allCardsData[(int) card];
            
            cardNameTMP.text =  cardData.cardFullName;
            cardBiomeTMP.text = cardData.biome.ToString();

            cardCategoryTMP.text = cardData.type is CardManager.CardType.Animal ? cardData.category.ToString() : cardData.type.ToString();
            
            cardEffectTMP.text = cardData.cardEffect;
            cardDescriptionTMP.text = cardData.cardDescription;
        }

        private void OnDisable()
        {
            cardPreview.SetActive(false);
        }
    }
}
