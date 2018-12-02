using System;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
    public TileGrid(Tile tile, int[,] map, Vector2 margin, Transform parent)
    {
        //Initialize a grid
        tiles = GridGeneration.GenerateTileGrid(tile, map, margin, parent);
    }

    private readonly Tile[,] tiles;
    private readonly Dictionary<int, Tile> idToTileMap = new Dictionary<int, Tile>(); //Maps an id to an x,y location on the grid for fast access to an ids location.

    //Starts tracking a unit on the grid.
    public bool StartTracking(int id, int startPosX, int startPosY)
    {
        if (idToTileMap.ContainsKey(id) || !IsValidTile(startPosX, startPosY)) return false;

        idToTileMap.Add(id, tiles[startPosX, startPosY]);
        tiles[startPosX, startPosY].StoredId = id;

        return true;
    }

    //Stops tracking a unit on the grid.
    public bool StopTracking(int id)
    {
        if (idToTileMap.ContainsKey(id)) idToTileMap[id].RemoveStoredId();

        return idToTileMap.Remove(id);
    }

    //Returns the world position of an id in the idToTileMap.
    public Vector3 GetPositionById(int id)
    {
        if (!idToTileMap.ContainsKey(id)) throw new Exception($"An id ({id}) that does not exist in the grid was accessed.");

        return idToTileMap[id].GetPositionWithOffset();
    }

    //Attempts to set the grid position of an id in the idToTileMap.
    public bool SetPositionOfId(int id, int x, int y)
    {
        if (!idToTileMap.ContainsKey(id)) throw new Exception($"An id ({id}) that does not exist in the grid was accessed.");

        if (!IsValidTile(x, y)) return false;

        idToTileMap[id].RemoveStoredId();
        idToTileMap[id] = tiles[x, y];
        tiles[x, y].StoredId = id;

        return true;
    }

    public bool IsValidTile(int x, int y)
    {
        return x < tiles.GetLength(0) && y < tiles.GetLength(1) && x >= 0 && y >= 0 && !tiles[x, y].IsOccupied;
    }
}
