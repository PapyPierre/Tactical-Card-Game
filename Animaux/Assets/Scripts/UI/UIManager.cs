using System.Collections.Generic;
using Board;
using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        private GameManager _gameManager;
        private CardManager _cardManager;
        private BoardManager _boardManager;

        [Space, SerializeField] private GameObject mainCanvas;

        [Space] public SwitchTurnMenu switchTurnMenu;
        [SerializeField] private GameObject endTurnBtn; 

        [Header("Card Info Display")] public CardInfoDisplayer cardInfoDisplayer;
        public float timeToShowCardInfo;

        [Space] public List<CardInHandDisplay> cardsInHandDisplays = new();

        [Space] public TextMeshProUGUI turnNumberTMP;
        
        [Space, SerializeField] private RectTransform player1PointsDisplay;
        [SerializeField] private RectTransform player2PointsDisplay;
        
        [SerializeField] private Vector3 leaderPointsDisplayPos = new Vector3(40, -50, 0);
        [SerializeField] private Vector3 otherPointsDisplayPos = new Vector3(-50, -210, 0);

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
            _boardManager = BoardManager.instance;
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

           var player1Score = _gameManager.players[0].currentPoints;
           var player2Score = _gameManager.players[1].currentPoints;
  
            if (player1Score > player2Score)
            {
                player1PointsDisplay.localPosition = leaderPointsDisplayPos;
                player2PointsDisplay.localPosition = otherPointsDisplayPos;
                
                player1PointsDisplay.localScale = Vector3.one * 1.5f;
                player2PointsDisplay.localScale = Vector3.one;
            }
            else if (player1Score < player2Score)
            {
                player2PointsDisplay.localPosition = leaderPointsDisplayPos;
                player1PointsDisplay.localPosition = otherPointsDisplayPos;
                
                player2PointsDisplay.localScale = Vector3.one * 1.5f;
                player1PointsDisplay.localScale = Vector3.one;
            }
        }

        public void UpdateCardInHandSprites()
        {
            foreach (var card in cardsInHandDisplays)
            {
                card.gameObject.SetActive(false);
            }

            for (var i = 0; i < _gameManager.currentPlayer.cardsInHand.Count; i++)
            {
                var cardInHandDisplay = cardsInHandDisplays[i];
                var cardIndex = (int)_gameManager.currentPlayer.cardsInHand[i];
                Sprite newSprite = _cardManager.allCardsData[cardIndex].sprite;
                
                cardInHandDisplay.gameObject.SetActive(true);
                cardInHandDisplay.image.sprite = newSprite;
            }
            
            foreach (var cardInHand in cardsInHandDisplays)
            {
                cardInHand.UnSelect();
            }
        }

        public void ResetCardInHandScale()
        {
            foreach (var card in cardsInHandDisplays)
            {
                card.ResetScale();
            }
        }

        public void ResetCardInHandColor()
        {
            foreach (var card in cardsInHandDisplays)
            {
                card.image.color = Color.white;
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

        public void ShowCardInfoDisplay(CardManager.Cards card)
        {
            _boardManager.tilesParent.gameObject.SetActive(false);
            _boardManager.playedCardsParent.gameObject.SetActive(false);
            mainCanvas.SetActive(false);
            
            cardInfoDisplayer.gameObject.SetActive(true);
            cardInfoDisplayer.SetUpInfos(card);
        }
    }
}