using System;
using UnityEngine;

public class UnitClickDetector : MonoBehaviour
{
    public event Action onClick;

    private void OnMouseDown()
    {
        onClick?.Invoke();
    }
}
