using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquad : Squad
{
    [SerializeField] private ChooseActionUI chooseActionUI;

    private Unit currentUnit;
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
            chooseActionUI.unitActionChosen += OnChooseActionChosen;
            chooseActionUI.skipTurn += OnSkipTurn;
            
            currentUnit.Visuals.SetSelectedMarkerColor();
        }
        else
        {
            InvokeAllUnitsPlayedEvent();
        }
    }

    private void OnChooseActionChosen(UnitAction action)
    {
        chooseActionUI.Hide();
        chooseActionUI.unitActionChosen -= OnChooseActionChosen;
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
        chooseActionUI.unitActionChosen -= OnChooseActionChosen;
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
        
        combatController.StartCombat(currentUnit, opponent, chosenAction, AttackSide.PLAYER_ATTACKS, OnCombatFinished);
    }

    private void OnCombatFinished()
    {
        currentUnit.PlayedInThisRound = true;
        currentUnit.Visuals.SetDeselectedMarkerColor();
        StartNextMove();
    }
}
