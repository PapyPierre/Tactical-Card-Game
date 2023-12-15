using Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Board
{
   public class Tile : MonoBehaviour
   {
      private MeshRenderer _meshRenderer; 
      public SpriteRenderer baseSR;
      public Image displayImage;
   
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