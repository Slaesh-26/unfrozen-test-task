using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Squad : MonoBehaviour
{
    public event Action allUnitsPlayed;
    
    [SerializeField] protected List<Unit> units;
    [SerializeField] protected Transform combatSpot;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color deselectedColor;
    [SerializeField] private Color defaultColor;
    
    protected CombatController combatController;
    protected List<Unit> opponents;
    protected Unit currentUnit;

    public virtual void Init(CombatController combatController)
    {
        this.combatController = combatController;

        foreach (Unit unit in units)
        {
            unit.Init(selectedColor, deselectedColor, defaultColor, combatSpot.position);
        }
    }

    public void StartTurn(List<Unit> opponents)
    {
        this.opponents = opponents;

        foreach (Unit unit in units)
        {
            unit.PlayedInThisRound = false;
        }

        StartNextMove();
    }

    public void EndTurn()
    {
        foreach (Unit unit in units)
        {
            unit.Visuals.SetDefaultMarkerColor();
        }
    }

    public List<Unit> GetActiveUnits()
    {
        return new List<Unit>(units);
    }

    protected virtual void StartNextMove() { }

    protected void InvokeAllUnitsPlayedEvent()
    {
        allUnitsPlayed?.Invoke();
    }

    protected bool TryPickRandomAvailableUnit(out Unit unit)
    {
        List<Unit> availableUnits = units.FindAll(u => !u.PlayedInThisRound);

        if (availableUnits.Count == 0)
        {
            unit = null;
            return false;
        }
        
        int random = Random.Range(0, availableUnits.Count);

        unit = availableUnits[random];
        return true;
    }
    
    protected void OnCombatFinished()
    {
        currentUnit.PlayedInThisRound = true;
        currentUnit.Visuals.SetDeselectedMarkerColor();
        StartNextMove();
    }
}
