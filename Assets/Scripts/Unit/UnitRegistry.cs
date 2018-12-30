using System.Collections.Generic;
using UnityEngine;

/*
 * Dictionary that stores Units mapped to their ids.
 * Used to access a Unit by its id.
 */
public class UnitRegistry : MonoBehaviour
{
    public static UnitRegistry Instance { get; private set; }

    private readonly Dictionary<int, Unit> idToUnitMap = new Dictionary<int, Unit>();

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
    }

    public bool StartTracking(Unit unit)
    {
        if (idToUnitMap.ContainsKey(unit.Id)) return false;

        idToUnitMap.Add(unit.Id, unit);

        return true;
    }

    public bool StopTracking(int id)
    {
        return idToUnitMap.Remove(id);
    }

    // Returns a Unit registered with the provided id or null if it doesn't exist
    public Unit GetUnit(int id)
    {
        if (idToUnitMap.TryGetValue(id, out Unit thisUnit))
        {
            return thisUnit;
        }

        return null;
    }
}