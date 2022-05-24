using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private List<Squad> squads;
    [SerializeField] private CombatController combatController;
    [SerializeField] private RoundInfoUI roundInfoUI;

    private int currentTurnSquadIndex = 0;
    private int round = 1;
    
    private void Start()
    {
        foreach (Squad squad in squads)
        {
            squad.allUnitsPlayed += OnAllUnitsPlayed;
            squad.Init(combatController);
        }
        
        squads[currentTurnSquadIndex].StartTurn(GetOpponents());
        roundInfoUI.SetRound(round);
    }

    private void OnDisable()
    {
        foreach (Squad squad in squads)
        {
            squad.allUnitsPlayed -= OnAllUnitsPlayed;
        }
    }

    private void OnAllUnitsPlayed()
    {
        squads[currentTurnSquadIndex].EndTurn();
        currentTurnSquadIndex++;
        
        if (currentTurnSquadIndex > squads.Count - 1)
        {
            currentTurnSquadIndex = 0;
            round++;
            
            roundInfoUI.SetRound(round);
        }
        
        squads[currentTurnSquadIndex].StartTurn(GetOpponents());
    }

    private List<Unit> GetOpponents()
    {
        List<Squad> otherSquads = GetOtherSquads(currentTurnSquadIndex);
        List<Unit> otherUnits = new List<Unit>();
        
        foreach (Squad squad in otherSquads)
        {
            otherUnits.AddRange(squad.GetActiveUnits());
        }

        return otherUnits;
    }

    private List<Squad> GetOtherSquads(int current)
    {
        List<Squad> otherSquads = new List<Squad>();
        
        for (int i = 0; i < squads.Count; i++)
        {
            if (i == current) continue;
            otherSquads.Add(squads[i]);
        }

        return otherSquads;
    }
}
