using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquad : Squad
{
    protected override void StartNextMove()
    {
        if (TryPickRandomAvailableUnit(out currentUnit))
        {
            currentUnit.Visuals.SetSelectedMarkerColor();
            StartCombat();
        }
        else
        {
            InvokeAllUnitsPlayedEvent();
        }
    }

    private void StartCombat()
    {
        int random = Random.Range(0, opponents.Count);
        Unit opponent = opponents[random];
        
        combatController.StartCombat(currentUnit, opponent, currentUnit.GetAction(), OnCombatFinished);
    }
}
