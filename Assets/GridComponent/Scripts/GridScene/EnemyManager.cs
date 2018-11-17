using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    //Maps an id to an x,y location on the grid.
    private Dictionary<int, Vector2Int> idToPositionMap = new Dictionary<int, Vector2Int>();
    private Tile[,] grid;

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
    }
}
