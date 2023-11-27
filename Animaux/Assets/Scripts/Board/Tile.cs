using Cards;
using UnityEngine;

namespace Board
{
   public class Tile : MonoBehaviour
   {
      private MeshRenderer _meshRenderer;
      public SpriteRenderer spriteRenderer;
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
      }

      private void OnMouseExit()
      {
         if (BoardManager.instance.mouseOverThisTile == this)
         {
            BoardManager.instance.mouseOverThisTile = null;
            _meshRenderer.material = baseMat;
         }
      }

      public bool IsLegalMove()
      {
         return cardOnThisTile == null;
      }
   }
}