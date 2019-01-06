using System;
using UnityEngine;

/*
 * Stores data and unit-related classes for other classes to access.
 * Units are created using the static method Unit.CreateAndSetupUnit().
 */
public class Unit : MonoBehaviour
{
    public int Id { get; private set; }
    private bool isInitialized = false;

    public MovementController movement;
    public UnitStats stats;

    private void Awake()
    {
        Id = GetInstanceID();
        movement = GetComponent<MovementController>();
        stats = new UnitStats(10);
    }

    // Creates a Unit from a prefab, assigns it to the grid and registers its id in the idToUnitMap. Returns the new Unit.
    public static Unit CreateAndSetupUnit(GameObject prefab, int gridLocationX, int gridLocationY, int id = -1)
    {
        // Create unit and set it up on the GridController
        Unit unit = CreateUnit(prefab);
        unit.Setup(gridLocationX, gridLocationY, id);

        return unit;
    }

    // Creates a Unit from a prefab. Returns the new Unit.
    private static Unit CreateUnit(GameObject prefab)
    {
        // Instantiate a copy of the prefab
        GameObject unitGameObject = Instantiate(prefab);
        Unit unit = unitGameObject.GetComponent<Unit>();

        // If we cannot find a Unit component, throw an error
        if (unit == null)
        {
            Destroy(unitGameObject);
            throw new Exception("Attempted to create a Unit from a prefab that doesn't contain the Unit script.");
        }

        return unit;
    }

    // Attempts to register the Unit with the GridController & UnitRegistry. Optionally allows a manual override for the id (this will be useful for networking).
    private bool Setup(int x, int y, int id = -1)
    {
        if (isInitialized) return false;

        if (id != -1) Id = id;

        // Registers this Unit in the GridController and UnitRegistry
        if (!GameComponents.GridController.StartTracking(this, x, y) || !GameComponents.UnitRegistry.StartTracking(this))
        {
            // If we failed to register with either, clean up and throw
            GameComponents.GridController.StopTracking(this);
            GameComponents.UnitRegistry.StopTracking(Id);
            Destroy(gameObject);

            throw new Exception($"A unit with the id {Id}({GetInstanceID()}) failed to spawn.");
        }

        transform.position = GameComponents.GridController.GetPosition(this);
        GameComponents.TurnHandler.AddUnitToTurnOrder(this);
        isInitialized = true;

        return true;
    }

    private void OnDestroy()
    {
        EventHandler.Invoke(Constants.EventName.UNITDESTROYED, this, new UnitEventArgs(this));
    }
}
