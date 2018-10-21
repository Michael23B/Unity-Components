using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GridController : MonoBehaviour
{
    public int Rows = 5;
    public int Columns = 5;
    public Vector3 StartPosition;
    public Vector2 GridMargin;

    public Tile Tile;

    public TileShape TileShape = TileShape.Hex;

    Tile[,] grid;
    Dictionary<int, Vector2Int> positionMap = new Dictionary<int, Vector2Int>();

    private void Awake()
    {
        grid = new Tile[Rows, Columns];
        GenerateTiles(Rows, Columns);
        EventHandler.Instance.StartListening(Constants.EventNames.TileHovered, OnTileHover);
    }

    //Starts tracking a unit on the grid
    public bool AddUnitToPositionMap(int unitId, int x, int y)
    {
        if (!IsValidTile(x, y) || positionMap.ContainsKey(unitId)) return false;

        positionMap.Add(unitId, new Vector2Int(x, y));

        return true;
    }

    //Stops tracking a unit
    public void RemoveUnitFromPositionMap(int unitId)
    {
        positionMap.Remove(unitId);
    }

    //Returns the position of the tile a unit is on or null if it isn't being tracked
    public Vector3? GetUnitsPosition(int unitId)
    {
        if (!positionMap.ContainsKey(unitId)) return null;

        Vector2Int currentPos = positionMap[unitId];

        return grid[currentPos.x, currentPos.y].GetPositionWithOffset();
    }

    //Returns the position of the tile a unit is trying to move to or null if that movement is invalid.
    public Vector3? GetUnitsNewPosition(int unitId, int xAmount, int yAmount)
    {
        if (!positionMap.ContainsKey(unitId)) return null;

        Vector2Int currentPos = positionMap[unitId];
        currentPos.x += xAmount;
        currentPos.y += yAmount;

        if (!IsValidTile(currentPos.x, currentPos.y)) return null;

        positionMap[unitId] = currentPos;

        return grid[currentPos.x, currentPos.y].GetPositionWithOffset();
    }

    private void GenerateTiles(int rows, int columns)
    {
        Vector3 startPos = StartPosition;

        for (int x = 0; x < rows; ++x)
        {
            for (int y = 0; y < columns; ++y)
            {
                Vector3 position = GetTilePosition(x, y, startPos);
                grid[x, y] = Instantiate(Tile, position, Quaternion.identity);
                grid[x, y].transform.parent = transform;
            }
        }
    }

    private Vector3 GetTilePosition(int x, int y, Vector3 startPos)
    {
        float xOffset = x * GridMargin.x;
        float yOffset = y * GridMargin.y;

        if (TileShape == TileShape.Hex)
        {
            xOffset = x * (Mathf.Sqrt(3) / 2 * GridMargin.x);
            if (y % 2 == 1) xOffset -= (Mathf.Sqrt(3) / 2 * GridMargin.x) / 2;
        }

        float tilePosX = startPos.x + xOffset;
        float tilePosY = startPos.z + yOffset;

        return new Vector3(tilePosX, startPos.y, tilePosY);
    }

    private bool IsValidTile(int x, int y)
    {
        if (x >= Rows || y >= Columns || x < 0 || y < 0) return false;
        return true;
    }

    //Events

    private void OnTileHover(Object sender, EventArgs e)
    {
        TileEventArgs eventArgs = (TileEventArgs)e;
        Raycast senderRaycast = (Raycast)sender;

        HoverEvent(eventArgs.Tile);
    }

    private void HoverEvent(Tile tile)
    {
        //Debug.Log($"Tile {tile.GetInstanceID()} hovered.");
    }
}

public enum TileShape
{
    Square,
    Hex
};