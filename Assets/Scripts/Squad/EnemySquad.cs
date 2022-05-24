using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquad : Squad
{
    private Unit currentUnit;
    
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
        
        combatController.StartCombat(currentUnit, opponent, currentUnit.GetAction(), 
                                     AttackSide.ENEMY_ATTACKS, OnCombatFinished);
    }

    private void OnCombatFinished()
    {
        currentUnit.PlayedInThisRound = true;
        currentUnit.Visuals.SetDeselectedMarkerColor();
        StartNextMove();
    }
}
