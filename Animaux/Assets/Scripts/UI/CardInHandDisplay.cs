using Cards;
using UnityEngine;

namespace UI
{
    public class CardInHandDisplay : MonoBehaviour
    {
        private bool isSelected;
        private Vector3 initScale;

        private const float animDuration = 0.5f;
        private float startTime;
        
        private void Start()
        {
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

        public void Select()
        {
            isSelected = true;
            startTime = Time.time;
        }

        public void UnSelect()
        {
            startTime = Time.time;
            isSelected = false;
        }
    }
}