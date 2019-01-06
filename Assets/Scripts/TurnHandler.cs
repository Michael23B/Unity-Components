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

    private void Awake()
    {
        Listener.CreateListener(transform, (sender, e) => UnitDestroyedEvent(((UnitEventArgs)e).Unit), Constants.EventName.UNITDESTROYED);
    }

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

        if (currentTurnIndex != -1)
        {
            if (currentTurnIndex >= unitsInRound.Count) currentTurnIndex = unitsInRound.Count - 1;

            EventHandler.Invoke(Constants.EventName.UNITTURNENDED, new TurnEventArgs(unitsInRound[currentTurnIndex], currentTurnIndex, unitsInRound.Count, totalTurns));
        }

        NextTurn();
    }

    public void AddUnitToTurnOrder(Unit unit)
    {
        unitsInRound.Add(unit);
    }

    public void RemoveUnitFromTurnOrder(Unit unit)
    {
        int unitIndex = unitsInRound.FindIndex(el => el.Id == unit.Id);

        if (unitIndex == -1) return;

        // If the unit we are removing has already taken a turn, the list will shift down so we need to decrease the current index.
        if (unitIndex < currentTurnIndex) currentTurnIndex--;

        unitsInRound.Remove(unit);
    }

    public Unit CurrentUnit()
    {
        return unitsInRound[currentTurnIndex];
    }

    public IReadOnlyCollection<Unit> GetTurnOrder()
    {
        return unitsInRound.AsReadOnly();
    }

    #region Events

    private void UnitDestroyedEvent(Unit unit)
    {
        RemoveUnitFromTurnOrder(unit);
    }

    #endregion
}
