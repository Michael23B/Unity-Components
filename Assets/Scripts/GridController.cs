using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController Instance { get; private set; }
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 5;
    [SerializeField] private Vector3 startPosition = new Vector3();
    [SerializeField] private Vector2 gridMargin = new Vector2(2.5f, -1.875f);
    [SerializeField] private Tile tile = null;
    //Maps an id to an x,y location on the grid.
    private Dictionary<int, Vector2Int> idToPositionMap = new Dictionary<int, Vector2Int>();
    private Tile[,] grid;

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);

        grid = new Tile[rows, columns];
        GenerateTiles(rows, columns, startPosition);
    }

    private void Start()
    {
        Listener.CreateListener(transform, (sender, e) => TileHoverEvent(((TileEventArgs)e).Tile), Constants.EventNames.TileHovered);
        Listener.CreateListener(transform, (sender, e) => TileClickEvent(((TileEventArgs)e).Tile), Constants.EventNames.TileClicked);
    }

    //Starts tracking a unit on the grid.
    public bool StartTracking(int id, int startPosX, int startPosY)
    {
        if (!IsValidTile(startPosX, startPosY) || idToPositionMap.ContainsKey(id)) return false;

        idToPositionMap.Add(id, new Vector2Int(startPosX, startPosY));
        grid[startPosX, startPosY].StoredId = id;

        return true;
    }

    //Stops tracking a unit on the grid.
    public bool StopTracking(int id)
    {
        return idToPositionMap.Remove(id);
    }

    //Returns the world position of an id in the idToPositionMap.
    public Vector3 GetPositionById(int id)
    {
        if (!idToPositionMap.ContainsKey(id))
        {
            Debug.LogError($"An id ({id}) that does not exist in the grid was accessed.");
            return new Vector3(-100, -100, -100);
        }

        Vector2Int currentPos = idToPositionMap[id];

        return grid[currentPos.x, currentPos.y].GetPositionWithOffset();
    }

    //Attempts to set the grid position of an id in the idToPositionMap.
    public bool SetPositionOfId(int id, int x, int y)
    {
        if (!idToPositionMap.ContainsKey(id) || !IsValidTile(x, y)) return false;

        Vector2Int currentPos = idToPositionMap[id];

        grid[currentPos.x, currentPos.y].RemoveStoredId();
        grid[x, y].StoredId = id;

        idToPositionMap[id] = new Vector2Int(x, y);

        return true;
    }

    public bool IsValidTile(int x, int y)
    {
        return x < rows && y < columns && x >= 0 && y >= 0 && !grid[x,y].IsOccupied;
    }

    #region Grid generation

    //Instantiates a number of Tiles in a grid formation. Expects hex shaped tiles.
    private void GenerateTiles(int rows, int columns, Vector3 startPosition)
    {
        for (int x = 0; x < rows; ++x)
        {
            for (int y = 0; y < columns; ++y)
            {
                Vector3 position = GetTilePosition(x, y, startPosition);
                grid[x, y] = Instantiate(tile, position, Quaternion.identity);
                grid[x, y].Setup(x, y);
                grid[x, y].transform.parent = transform;
            }
        }
    }

    //Calculates a tiles world position based on starting position, margin and the current coordinates.
    private Vector3 GetTilePosition(int x, int y, Vector3 startPos)
    {
        float xOffset = x * (Mathf.Sqrt(3) / 2 * gridMargin.x);
        float yOffset = y * gridMargin.y;

        if (y % 2 == 1) xOffset -= (Mathf.Sqrt(3) / 2 * gridMargin.x) / 2;

        float tilePosX = startPos.x + xOffset;
        float tilePosY = startPos.z + yOffset;

        //Z-axis in unity is up/down but since we are working in a 2d grid, we call it Y.
        return new Vector3(tilePosX, startPos.y, tilePosY);
    }

    #endregion

    #region Events

    private void TileHoverEvent(Tile tile)
    {
        //Debug.Log("Hover event called");
    }

    private void TileClickEvent(Tile tile)
    {
        //Debug.Log("Click event called");
    }

    #endregion
}