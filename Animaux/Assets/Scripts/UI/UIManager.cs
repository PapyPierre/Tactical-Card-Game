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
        public SwitchTurnMenu switchTurnMenu;
        public CardInfoDisplayer cardInfoDisplayer;
        public List<Image> cardInHandSprite;
        public List<CardInHandDisplay> cardInHandDisplays = new ();
        public TextMeshProUGUI turnNumberTMP;

        private void Start()
        {
            _gameManager = GameManager.instance;
            _cardManager = CardManager.instance;
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
                var cardIndex = (int) _gameManager.currentPlayer.cardsInHand[i];
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
    }
}