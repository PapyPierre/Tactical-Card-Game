using System.Collections.Generic;
using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        private GameManager _gameManager;
        private CardManager _cardManager;

        [Space] public SwitchTurnMenu switchTurnMenu;
        [SerializeField] private GameObject endTurnBtn; 

        [Space] public CardInfoDisplayer cardInfoDisplayer;

        [Space] public List<Image> cardInHandSprite;
        public List<CardInHandDisplay> cardInHandDisplays = new();

        [Space] public TextMeshProUGUI turnNumberTMP;

        [Space, SerializeField] private Slider pointsSliderPlayer1;
        [SerializeField] private Slider pointsSliderPlayer2;

        [Space, SerializeField] private TextMeshProUGUI pointsTMPPlayer1;
        [SerializeField] private TextMeshProUGUI pointsTMPPlayer2;

        [SerializeField] private GameObject endScreen;
        [SerializeField] private TextMeshProUGUI endTMP;
        
        private void Start()
        {
            _gameManager = GameManager.instance;
            _cardManager = CardManager.instance;
        }

        public void UpdateSliderPoints(int playerIndex, uint scoreValue)
        {
            if (scoreValue > 100)
            {
                scoreValue = 100;
            }

            switch (playerIndex)
            {
                case 1:
                    pointsSliderPlayer1.value = scoreValue;
                    pointsTMPPlayer1.text = scoreValue.ToString();
                    break;
                case 2:
                    pointsSliderPlayer2.value = scoreValue;
                    pointsTMPPlayer2.text = scoreValue.ToString();
                    break;
                default:
                    Debug.LogError($"{playerIndex} is not correct");
                    break;
            }
        }

        public void UpdateCardInHandSprites()
        {
            foreach (var image in cardInHandSprite)
            {
                image.gameObject.SetActive(false);
            }

            for (var i = 0; i < _gameManager.currentPlayer.cardsInHand.Count; i++)
            {
                Image image = cardInHandSprite[i];
                image.gameObject.SetActive(true);
                var cardIndex = (int)_gameManager.currentPlayer.cardsInHand[i];
                Sprite newSprite = _cardManager.allCardsData[cardIndex].sprite;
                image.sprite = newSprite;
            }
        }

        public void ResetCardInHandColor()
        {
            foreach (var image in cardInHandSprite)
            {
                image.color = Color.white;
            }
        }

        public void ShowWinText(string text)
        {
            endScreen.SetActive(true);
            endTMP.text = text;
        }

        public void SetActiveEndTurnBtn(bool value)
        {
            endTurnBtn.SetActive(value);
        }
    }
}