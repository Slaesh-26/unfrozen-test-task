using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 initialPos;
    private Vector3 combatSpot;

    public void Init(Vector3 combatSpot)
    {
        initialPos = transform.position;
        this.combatSpot = combatSpot;
    }

    public IEnumerator MoveTo(Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);
            
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator MoveToInitialPos()
    {
        yield return MoveTo(initialPos);
    }
    
    public IEnumerator MoveToCombatSpot()
    {
        yield return MoveTo(combatSpot);
    }
}
