using System;
using UnityEngine;
using Object = UnityEngine.Object;

/*
 * Separates the Unit class from it's creation and set up. Registers the unit in other places it's needed.
 */
public static class UnitCreation
{
    // Creates a Unit from a prefab. Returns the new Unit.
    private static Unit CreateUnit(GameObject prefab)
    {
        // Instantiate a copy of the prefab
        GameObject unitGameObject = Object.Instantiate(prefab);
        Unit unit = unitGameObject.GetComponent<Unit>();

        // If we cannot find a Unit component, throw an error
        if (unit == null)
        {
            Object.Destroy(unitGameObject);
            throw new Exception("Attempted to create a Unit from a prefab that doesn't contain the Unit script.");
        }

        return unit;
    }

    // Attempts to register the Unit with the GridController & UnitRegistry. Optionally allows a manual override for the id (this will be useful for networking).
    private static bool Setup(Unit unit, int x, int y, int id = -1)
    {
        if (id != -1) unit.OverrideId(id);

        // Registers this Unit in the GridController and UnitRegistry
        if (!GameComponents.GridController.StartTracking(unit, x, y) || !GameComponents.UnitRegistry.StartTracking(unit))
        {
            // If we failed to register with either, clean up and throw
            GameComponents.GridController.StopTracking(unit);
            GameComponents.UnitRegistry.StopTracking(unit.Id);
            Object.Destroy(unit.gameObject);

            throw new Exception($"A unit with the id {unit.Id}({unit.GetInstanceID()}) failed to spawn.");
        }

        unit.transform.position = GameComponents.GridController.GetPosition(unit);
        GameComponents.TurnHandler.AddUnitToTurnOrder(unit);

        return true;
    }

    // Creates a Unit from a prefab, assigns it to the grid and registers its id in the idToUnitMap. Returns the new Unit.
    public static Unit CreateAndSetupUnit(GameObject prefab, int gridLocationX, int gridLocationY, int id = -1)
    {
        // Create unit and set it up on the GridController
        Unit unit = CreateUnit(prefab);
        Setup(unit, gridLocationX, gridLocationY, id);

        return unit;
    }
}
