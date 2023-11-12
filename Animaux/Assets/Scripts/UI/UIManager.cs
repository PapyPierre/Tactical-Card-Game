using System.Collections.Generic;
using Cards;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        private GameManager _gameManager;
        private CardManager _cardManager;
    
        public List<Image> cardInHandSprite;

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
            
            for (var i = 0; i < _gameManager.currentPlayingPlayer.cardsInHand.Count; i++)
            {
                var image = cardInHandSprite[i];
                image.gameObject.SetActive(true);
                var cardIndex = (int) _gameManager.currentPlayingPlayer.cardsInHand[i];
                Sprite newSprite = _cardManager.allCardsData[cardIndex].sprite;
                image.sprite = newSprite;
            }
        }
    
        // Called from UI
        public void PlayCardInHand(int i)
        {
            _gameManager.currentPlayingPlayer.SelectCardInHand(i);

            ResetCardInHandColor();
            
            cardInHandSprite[i].color = _gameManager.currentPlayingPlayer.playerColor;
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