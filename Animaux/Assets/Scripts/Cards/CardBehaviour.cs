using UnityEngine;

namespace Cards
{
    public class CardBehaviour : MonoBehaviour
    {
        [SerializeField] protected CardData data;
        
        private void Start()
        {
            OnPose();
        }

        protected virtual void OnPose()
        {
            
        }
        
        protected virtual void OnScoreCompute()
        {
            
        }
    }
}
