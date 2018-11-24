using System.Collections.Generic;
using UnityEngine;

public class UnitRegistry : MonoBehaviour
{
    public static UnitRegistry Instance { get; private set; }

    private readonly Dictionary<int, Unit> idToUnitMap = new Dictionary<int, Unit>();

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
    }

    public bool StartTracking(int id, Unit unit)
    {
        if (idToUnitMap.ContainsKey(id)) return false;

        idToUnitMap.Add(id, unit);

        return true;
    }

    public bool StopTracking(int id)
    {
        return idToUnitMap.Remove(id);
    }

    //Returns a Unit registered with the provided id or null if it doesn't exist
    public Unit GetUnit(int id)
    {
        if (idToUnitMap.TryGetValue(id, out Unit thisUnit))
        {
            return thisUnit;
        }

        return null;
    }
}