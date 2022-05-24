using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAction : ScriptableObject
{
    [SerializeField] private string text;
    
    public abstract void DoAction(Unit actor, Unit opponent);

    public virtual string GetText()
    {
        return text;
    }
}
