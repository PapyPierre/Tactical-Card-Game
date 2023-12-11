using Cards;
using UnityEngine;

namespace Board
{
   public class Tile : MonoBehaviour
   {
      private MeshRenderer _meshRenderer; 
      public SpriteRenderer baseSR;
      public SpriteRenderer overlapSR;
   
      [HideInInspector] public CardBehaviour cardOnThisTile;

      private void Start()
      {
         _meshRenderer = GetComponent<MeshRenderer>();
      }

      public bool IsLegalMove()
      {
         return cardOnThisTile == null;
      }

      public void SetMat(Material mat)
      {
         _meshRenderer.material = mat;
      }
   }
}