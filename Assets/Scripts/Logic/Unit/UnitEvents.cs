using System.Linq;
using UnityEngine;

/*
 * Selects Units when a tile that has an id is clicked.
 * Listens for events and manipulates the selected Unit.
 */
public class UnitEvents : MonoBehaviour
{
    private Unit selectedUnit;

    private void Start()
    {
        ListenerCreation.CreateListener(transform, (sender, e) => TileHoverEvent(((TileEventArgs)e).Tile), Constants.EventName.TILEHOVERED);
        ListenerCreation.CreateListener(transform, (sender, e) => TileClickEvent(((TileEventArgs)e).Tile), Constants.EventName.TILECLICKED);
        ListenerCreation.CreateListener(transform, (sender, e) => TileRightClickEvent(((TileEventArgs)e).Tile), Constants.EventName.TILERIGHTCLICKED);
        ListenerCreation.CreateListener(transform, (sender, e) => UnitDestroyedEvent(((UnitEventArgs)e).Unit), Constants.EventName.UNITDESTROYED);
        ListenerCreation.CreateListener(transform, (sender, e) => UnitTurnStartedEvent(((TurnEventArgs)e).CurrentUnit), Constants.EventName.UNITTURNSTARTED);
    }

    private void SelectUnit(int unitId)
    {
        selectedUnit = GameComponents.UnitRegistry.GetUnit(unitId);
    }

    private void DeselectUnit()
    {
        selectedUnit = null;
    }

    private bool CanMoveUnit(Unit unit)
    {
        return unit
               && unit == GameComponents.GameState.ActiveUnit
               && unit.Stats.Team == 1;
    }

    #region Events

    private void TileHoverEvent(Tile tile)
    {
        // TODO Face the unit in the direction of the tile
    }

    private void UnitDestroyedEvent(Unit unit)
    {
        if (unit.Id == selectedUnit?.Id) DeselectUnit();

        // If we are not quitting, clean up any remaining links to the Unit
        if (!GameComponents.GameState.IsApplicationQuitting)
        {
            GameComponents.GridController.StopTracking(unit);
            GameComponents.UnitRegistry.StopTracking(unit);
        }
    }

    private void TileClickEvent(Tile tile)
    {
        // If the tile is occupied, select the unit stored in that tile
        if (tile.IsOccupied)
        {
            SelectUnit(tile.StoredId);
            return;
        }

        // If we have a unit and it is the active unit, move it
        if (CanMoveUnit(selectedUnit))
        {
            GameComponents.GridController.MoveUnit(selectedUnit, tile);
            GameComponents.TurnHandler.EndTurn();
        }
        // TODO else give feedback for not being able to move
    }

    private void TileRightClickEvent(Tile tile)
    {
        // If we have a unit and the target tile has a different unit, destroy it
        if (selectedUnit && tile.IsOccupied && tile.StoredId != selectedUnit.Id)
        {
            Unit target = GameComponents.UnitRegistry.GetUnit(tile.StoredId);
            Destroy(target.gameObject);
        }
    }

    private void UnitTurnStartedEvent(Unit unit)
    {
        // If the Unit is on the enemy team, perform a random action then end it's turn
        if (unit.Stats.Team == 2)
        {
            if (GameComponents.TurnHandler.GetTurnOrder().All(unitInTurn => unitInTurn.Stats.Team == 2)) return; // Only enemies left, don't want to keep calling AI

            UnitAction action = AI.GetAction(unit);

            if (action.ActionType == Constants.ActionType.MOVE)
            {
                GameComponents.GridController.MoveUnit(unit, action.Target);
            }
            else if (action.ActionType == Constants.ActionType.ATTACK)
            {
                // not implemented
            }

            // TODO wait until the unit has finished its turn (animation/particle effects etc.)

            GameComponents.TurnHandler.EndTurn();
        }
    }

    #endregion
}