using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Id { get; private set; }
    private bool initialized = false;

    public MovementController movement;

    private void Awake()
    {
        Id = GetInstanceID();
        movement = GetComponent<MovementController>();
    }

    //Creates a Unit from a prefab, assigns it to the grid and registers its id in the idToUnitMap. Returns the new Unit
    public static Unit CreateAndRegisterUnit(GameObject prefab, int gridLocationX, int gridLocationY, int id = -1)
    {
        //Create unit and set it up on the GridController
        Unit unit = CreateUnit(prefab);
        unit.Setup(gridLocationX, gridLocationY, id);

        return unit;
    }


    //Attempts to spawn a unit and place it on the grid. Optionally allows a manual override for the id (this will be useful for networking).
    private bool Setup(int x, int y, int id = -1)
    {
        if (initialized) return false;

        if (id != -1) Id = id;

        //Registers this Unit in the GridController and UnitRegistry
        if (!GridController.Instance.StartTracking(Id, x, y) || !UnitRegistry.Instance.StartTracking(Id, this))
        {
            //If we failed to register with either, clean up and throw
            GridController.Instance.StopTracking(Id);
            UnitRegistry.Instance.StopTracking(Id);

            throw new Exception($"A unit with the id {Id}({GetInstanceID()}) failed to spawn.");
        }

        transform.position = GridController.Instance.GetPositionById(Id);
        initialized = true;

        return true;
    }

    private static Unit CreateUnit(GameObject prefab)
    {
        //Instantiate a copy of the prefab
        GameObject unitGameObject = Instantiate(prefab);
        Unit unit = unitGameObject.GetComponent<Unit>();

        //If we cannot find a Unit component, throw an error
        if (unit == null) throw new Exception("Attempted to create a Unit from a prefab that doesn't contain the Unit script.");

        return unit;
    }

    private void OnDestroy()
    {
        GridController.Instance.StopTracking(Id);
        UnitRegistry.Instance.StopTracking(Id);
    }
}
