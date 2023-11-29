using Cards;
using UI;
using UnityEngine;

namespace Board
{
   public class Tile : MonoBehaviour
   {
      private MeshRenderer _meshRenderer; 
      public SpriteRenderer baseSR;
      public SpriteRenderer overlapSR;

      [SerializeField] private Material baseMat;
      [SerializeField] private Material overMat;
   
      [HideInInspector] public CardBehaviour cardOnThisTile;

      private void Start()
      {
         _meshRenderer = GetComponent<MeshRenderer>();
      }

      private void OnMouseEnter()
      {
         BoardManager.instance.mouseOverThisTile = this;
         _meshRenderer.material = overMat;

         if (GameManager.instance.currentPlayer.selectedCardInHand == CardManager.Cards.Uninitialised && cardOnThisTile)
         {
            UIManager.instance.cardInfoDisplayer.ShowCardInfoDisplay(cardOnThisTile.data.thisCard);
         }
      }

      private void OnMouseExit()
      {
         if (BoardManager.instance.mouseOverThisTile == this)
         {
            BoardManager.instance.mouseOverThisTile = null;
            _meshRenderer.material = baseMat;
            if (GameManager.instance.currentPlayer.selectedCardInHand == CardManager.Cards.Uninitialised)
            {
               UIManager.instance.cardInfoDisplayer.HideCardInfoDisplay();
            }
         }
      }

      public bool IsLegalMove()
      {
         return cardOnThisTile == null;
      }
   }
}