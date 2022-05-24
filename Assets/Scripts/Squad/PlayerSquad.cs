using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquad : Squad
{
    [SerializeField] private ChooseActionUI chooseActionUI;
    
    private UnitAction chosenAction;

    public override void Init(CombatController combatController)
    {
        base.Init(combatController);
        chooseActionUI.Init();
    }

    protected override void StartNextMove()
    {
        if (TryPickRandomAvailableUnit(out currentUnit))
        {
            chooseActionUI.Show(currentUnit.GetAction());
            chooseActionUI.unitActionChosen += OnChooseActionPicked;
            chooseActionUI.skipTurn += OnSkipTurn;
            
            currentUnit.Visuals.SetSelectedMarkerColor();
        }
        else
        {
            InvokeAllUnitsPlayedEvent();
        }
    }

    private void OnChooseActionPicked(UnitAction action)
    {
        chooseActionUI.Hide();
        chooseActionUI.unitActionChosen -= OnChooseActionPicked;
        chooseActionUI.skipTurn -= OnSkipTurn;

        foreach (Unit opponent in opponents)
        {
            opponent.onClick += OnOpponentClicked;
        }

        chosenAction = action;
    }

    private void OnSkipTurn()
    {
        chooseActionUI.Hide();
        chooseActionUI.unitActionChosen -= OnChooseActionPicked;
        chooseActionUI.skipTurn -= OnSkipTurn;
        
        OnCombatFinished();
    }

    private void OnOpponentClicked(Unit opponent)
    {
        opponent.Visuals.PlaySelectedAnimation();
        
        foreach (Unit unit in opponents)
        {
            unit.onClick -= OnOpponentClicked;
        }
        
        combatController.StartCombat(currentUnit, opponent, chosenAction, OnCombatFinished);
    }
}
