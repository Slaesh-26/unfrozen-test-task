using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClickDetector : MonoBehaviour
{
    public event Action onClick;

    private void OnMouseDown()
    {
        onClick?.Invoke();
        print("Clicked");
    }
}
