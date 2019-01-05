using System.Collections.Generic;
using UnityEngine;
/*
 * Stores turn order and fires turn related events.
 */
public class TurnHandler : MonoBehaviour
{
    private readonly List<Unit> unitsInRound = new List<Unit>();
    private int currentTurnIndex = -1;
    private int totalTurns = 0;

    public void StartGame()
    {
        currentTurnIndex = -1;
        totalTurns = 0;
    }

    void NextTurn()
    {
        if (unitsInRound.Count == 0) return;
        currentTurnIndex++;
        totalTurns++;

        if (currentTurnIndex >= unitsInRound.Count) NextRound();

        EventHandler.Invoke(Constants.EventName.UNITTURNSTARTED, this, new TurnEventArgs(unitsInRound[currentTurnIndex], currentTurnIndex, unitsInRound.Count, totalTurns));
    }

    void NextRound()
    {
        EventHandler.Invoke(Constants.EventName.ROUNDENDED);

        currentTurnIndex = 0;

        // TODO Calculate new turn order

        EventHandler.Invoke(Constants.EventName.ROUNDSTARTED);
    }

    public void EndTurn()
    {
        if (unitsInRound.Count == 0) return;

        if (currentTurnIndex != -1) EventHandler.Invoke(Constants.EventName.UNITTURNENDED, new TurnEventArgs(unitsInRound[currentTurnIndex], currentTurnIndex, unitsInRound.Count, totalTurns));

        NextTurn();
    }

    public void AddUnitToGame(Unit unit)
    {
        unitsInRound.Add(unit);
    }

    public Unit CurrentUnit()
    {
        return unitsInRound[currentTurnIndex];
    }

    public IReadOnlyCollection<Unit> GetTurnOrder()
    {
        return unitsInRound.AsReadOnly();
    }
}
