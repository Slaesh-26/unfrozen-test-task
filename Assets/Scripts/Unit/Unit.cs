using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public event Action<Unit> onClick;
    public bool PlayedInThisRound { get; set; }

    public UnitVisuals Visuals => unitVisuals;
    public UnitMovement Movement => unitMovement;

    [SerializeField] private UnitClickDetector clickDetector;
    [SerializeField] private UnitMovement unitMovement;
    [SerializeField] private UnitVisuals unitVisuals;
    [Space]
    [SerializeField] private UnitAction unitAction;

    private Vector3 combatSpot;

    public void Init(Color selectedColor, Color deselectedColor, Color defaultColor, Vector3 combatSpot)
    {
        clickDetector.onClick += OnClick;
        unitMovement.Init(combatSpot);
        unitVisuals.Init(selectedColor, deselectedColor, defaultColor);
    }

    public UnitAction GetAction()
    {
        return unitAction;
    }

    private void OnClick()
    {
        onClick?.Invoke(this);
    }

    private void OnDisable()
    {
        clickDetector.onClick -= OnClick;
    }
}
