using System.Collections.Generic;
using UnityEngine;
/*
 * Stores turn order and fires turn related events.
 */
public class TurnHandler : MonoBehaviour
{
    private readonly List<Unit> units = new List<Unit>();
    private int currentTurnIndex = -1;

    public void StartGame()
    {
        currentTurnIndex = -1;
        NextRound();
    }

    void NextTurn()
    {
        if (units.Count == 0) return;
        currentTurnIndex++;

        if (currentTurnIndex >= units.Count) NextRound();

        EventHandler.Invoke(Constants.EventName.UNITTURNSTARTED, new UnitEventArgs(units[currentTurnIndex]));
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
        EventHandler.Invoke(Constants.EventName.UNITTURNENDED, new UnitEventArgs(units[currentTurnIndex]));
        NextTurn();
    }

    public void AddUnitToGame(Unit unit)
    {
        units.Add(unit);
    }

    public Unit CurrentUnit()
    {
        return units[currentTurnIndex];
    }

    public IReadOnlyCollection<Unit> GetTurnOrder()
    {
        return units.AsReadOnly();
    }
}
