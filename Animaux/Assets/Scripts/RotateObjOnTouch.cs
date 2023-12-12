using UnityEngine;

public class RotateObjectOnTouch : MonoBehaviour
{
    private bool isRotating = false;
    private Vector2 touchStartPos;

    public float rotationSpeed = 2.0f;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Enregistrez la position du toucher au début
                    touchStartPos = touch.position;
                    isRotating = true;
                    break;

                case TouchPhase.Moved:
                    // Calculez la différence de position entre le début et la position actuelle
                    float deltaPositionX = touch.position.x - touchStartPos.x;

                    // Appliquez la rotation à l'objet en fonction du mouvement du doigt
                    transform.Rotate(Vector3.up, -deltaPositionX * rotationSpeed * Time.deltaTime);
                    touchStartPos = touch.position; // Mettez à jour la position du toucher
                    break;

                case TouchPhase.Ended:
                    isRotating = false;
                    break;
            }
        }
        else
        {
            isRotating = false;
        }
    }
}