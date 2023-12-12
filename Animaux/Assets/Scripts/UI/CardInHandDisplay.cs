using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class CardInHandDisplay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private GameManager _gameManager;
        private UIManager _uiManager;
            
        [HideInInspector] public Image image;
        
        private bool isHoldingOverElement;
        private float holdTime;
        
        private bool isSelected;
        
        private Vector3 initScale;
        private const float animDuration = 0.5f;
        private float startTime;
        
        [SerializeField] private int indexInHand;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Start()
        {
            _gameManager = GameManager.instance;
            _uiManager = UIManager.instance;
            initScale = transform.localScale;
        }

        private void Update()
        {
            if (isSelected)
            {
                ScaleUp();
            }
            else
            {
               ScaleDown();
            }
            
            if (isHoldingOverElement) holdTime += Time.deltaTime;

            if (holdTime > _uiManager.timeToShowCardInfo)
            {
                _uiManager.ShowCardInfoDisplay(_gameManager.currentPlayer.cardsInHand[indexInHand]);
                isHoldingOverElement = false;
                holdTime = 0;
            }
        }

        private void ScaleUp()
        {
            transform.localScale = Vector3.Slerp(transform.localScale, initScale * 1.4f,
                (Time.time - startTime) / animDuration);
        }

        private void ScaleDown()
        {
            transform.localScale = Vector3.Slerp(transform.localScale, initScale,
                (Time.time - startTime) / animDuration);
        }

        public void ResetScale()
        {
            transform.localScale = initScale;
        }

        private void Select()
        {
            _gameManager.currentPlayer.SelectCardInHand(indexInHand);

            isSelected = true;
            startTime = Time.time;
            
            _uiManager.ResetCardInHandColor();
            image.color = _gameManager.currentPlayer.playerColor;
        }

        public void UnSelect()
        {
            startTime = Time.time;
            isSelected = false;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            isHoldingOverElement = true;
          
            Select();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            isHoldingOverElement = false;
        }
    }
}