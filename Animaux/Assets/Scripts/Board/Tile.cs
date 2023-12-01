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

      public bool IsLegalMove()
      {
         return cardOnThisTile == null;
      }
   }
}