using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Board
{
   public class Tile : MonoBehaviour
   {
      private MeshRenderer _meshRenderer; 
      public SpriteRenderer baseSR;
      [SerializeField] private GameObject canvas;
      [SerializeField] private Image pointsDisplayImage;
      [SerializeField] private TextMeshProUGUI pointsDisplayTmp;
   
      [HideInInspector] public CardBehaviour cardOnThisTile;

      private void Start()
      {
         _meshRenderer = GetComponent<MeshRenderer>();
         canvas.SetActive(false);
      }

      public bool IsLegalMove()
      {
         return cardOnThisTile == null;
      }

      public void SetMat(Material mat)
      {
         _meshRenderer.material = mat;
      }

      public void SetPointsImageColor(Color color)
      {
         canvas.SetActive(true);
         pointsDisplayImage.color = color;
      }

      public void UpdatePointsDisplay()
      {
         if (cardOnThisTile == null)
         {
            canvas.SetActive(false);
            return;
         }

         canvas.SetActive(true);
         pointsDisplayTmp.text = cardOnThisTile.IsNegate() ? "X" : cardOnThisTile.CurrentPointsValue().ToString();
      }
   }
}