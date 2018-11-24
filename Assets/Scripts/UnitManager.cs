using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }
    //Maps an id to an x,y location on the grid.
    private Dictionary<int, Unit> idToUnitMap = new Dictionary<int, Unit>();

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
    }
}