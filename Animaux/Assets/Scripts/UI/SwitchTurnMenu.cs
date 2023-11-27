using TMPro;
using UnityEngine;

namespace UI
{
   public class SwitchTurnMenu : MonoBehaviour
   {
      [SerializeField] private TextMeshProUGUI playerTurnText;

      public void Show(Player nextPlayerPlaying)
      {
         gameObject.SetActive(true);
         playerTurnText.text = nextPlayerPlaying.name;
         nextPlayerPlaying.hasPlayedACardThisTurn = false;
      }
      
      public void SwitchPlayerTurn()
      {
         gameObject.SetActive(false);
         GameManager.instance.StartNextPlayerTurn();
      }
   }
}
