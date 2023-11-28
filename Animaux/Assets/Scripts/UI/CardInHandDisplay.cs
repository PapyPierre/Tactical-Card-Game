using System;
using Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class CardInHandDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool isMouseOver;
        private Vector3 initScale;
        [HideInInspector] public bool isLocked;
        
        private const float animDuration = 1.0f;
        private float startTime;

        [SerializeField] private int index;

        private void Start()
        {
            initScale = transform.localScale;
        }

        private void Update()
        {
            if (isMouseOver)
            {
                transform.localScale = Vector3.Slerp(transform.localScale, initScale * 1.4f, 
                    (Time.time - startTime) / animDuration);
            }
            else
            {
                if (!isLocked)
                {
                    transform.localScale = Vector3.Slerp(transform.localScale, initScale, 
                        (Time.time - startTime) / animDuration);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOver = true;
            startTime = Time.time;
            
            if (GameManager.instance.currentPlayer.selectedCardInHand == CardManager.Cards.Uninitialised)
            {
                UIManager.instance.cardInfoDisplayer.ShowCardInfoDisplay(GameManager.instance.currentPlayer.cardsInHand[index]);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            startTime = Time.time;
            isMouseOver = false;
            
            if (GameManager.instance.currentPlayer.selectedCardInHand == CardManager.Cards.Uninitialised)
            {
                UIManager.instance.cardInfoDisplayer.HideCardInfoDisplay();
            }
        }
        
        private void OnDisable()
        {
            isLocked = false;
        }
    }
}