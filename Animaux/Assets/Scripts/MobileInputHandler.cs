using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileInputHandler : MonoBehaviour
{
    public void OnTap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            // Player taped once the screen at ctx.ReadValue<Vector2>()

          GameManager.instance.CheckToPlayCard(ctx.ReadValue<Vector2>());
        }
    }

    public void OnHoldTouch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            // Player is holding touch at ctx.ReadValue<Vector2>() (position on screen)
        }
    }


    public void OnReleaseTouch(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            // Player release touch
        }
    }
}
