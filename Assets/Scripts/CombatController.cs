using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public void StartCombat(Unit attacker, Unit attacked, UnitAction chosenAction, Action callback)
    {
        StartCoroutine(StartCombatInternal(attacker, attacked, chosenAction, callback));
    }

    private IEnumerator StartCombatInternal(Unit attacker, Unit attacked, UnitAction chosenAction, Action finishedCallback)
    {
        yield return attacker.Movement.MoveToCombatSpot();
        yield return attacked.Movement.MoveToCombatSpot();
        
        chosenAction.DoAction(attacker, attacked);
        
        attacker.Visuals.AttackAnimation();
        yield return new WaitForSeconds(0.5f);
        attacked.Visuals.GetDamageAnimation();
        yield return new WaitForSeconds(1f);


        yield return attacker.Movement.MoveToInitialPos();
        yield return attacked.Movement.MoveToInitialPos();
        
        attacker.Visuals.SetDeselectedMarkerColor();
        
        finishedCallback.Invoke();
    }
}
