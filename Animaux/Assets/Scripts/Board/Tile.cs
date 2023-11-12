using Board;
using Cards;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
   private MeshRenderer _meshRenderer;
   public SpriteRenderer spriteRenderer;
   [SerializeField] private Material baseMat;
   [SerializeField] private Material overMat;
   
   [FormerlySerializedAs("cardOnThisTile")] [HideInInspector] public CardData cardDataOnThisTile;

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
      return cardDataOnThisTile == null;
   }
}