using System;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    public Action OnPose;

    private void Start()
    {
        OnPose();
    }
}
