using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    
    public void StartCombat(Unit attacker, Unit attacked, UnitAction chosenAction,AttackSide attackSide, Action callback)
    {
        StartCoroutine(StartCombatInternal(attacker, attacked, chosenAction, attackSide, callback));
    }

    private IEnumerator StartCombatInternal(Unit attacker, Unit attacked, UnitAction chosenAction, 
                                            AttackSide attackSide, Action finishedCallback)
    {
        if (attackSide == AttackSide.PLAYER_ATTACKS)
        {
            yield return attacker.Movement.MoveTo(leftPos.position);
            yield return attacked.Movement.MoveTo(rightPos.position);
        }
        else if (attackSide == AttackSide.ENEMY_ATTACKS)
        {
            yield return attacker.Movement.MoveTo(rightPos.position);
            yield return attacked.Movement.MoveTo(leftPos.position);
        }
        
        chosenAction.DoAction(attacker, attacked);
        
        attacker.Visuals.Attack();
        yield return new WaitForSeconds(0.5f);
        attacked.Visuals.GetDamage();
        yield return new WaitForSeconds(1f);


        yield return attacker.Movement.MoveToInitialPos();
        yield return attacked.Movement.MoveToInitialPos();
        
        attacker.Visuals.SetDeselectedMarkerColor();
        
        finishedCallback.Invoke();
    }
}

public enum AttackSide
{
    PLAYER_ATTACKS,
    ENEMY_ATTACKS
}
