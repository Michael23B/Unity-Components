using System.Collections.Generic;
using UnityEngine;

/*
 * Stores data to be used by other classes.
 * Stores an array of Tiles as well as a dictionary that maps an id to a specific tile.
 * Should only be accessed by the related helper class, GridController, since the logic of manipulating the dictionary should be separate from more specific game logic.
 */
public class TileGrid
{
    public TileGrid(Tile tile, int[,] map, Vector2 margin, Transform parent)
    {
        // Initialize a grid
        tiles = GridGeneration.GenerateTileGrid(tile, map, margin, parent);
    }

    private readonly Tile[,] tiles;
    private readonly Dictionary<int, Tile> idToTileMap = new Dictionary<int, Tile>();

    // Starts tracking a unit on the grid.
    public bool StartTracking(int id, int startPosX, int startPosY)
    {
        if (idToTileMap.ContainsKey(id) || !IsTileOccupied(startPosX, startPosY)) return false;

        idToTileMap.Add(id, tiles[startPosX, startPosY]);
        tiles[startPosX, startPosY].StoredId = id;

        return true;
    }

    // Stops tracking a unit on the grid.
    public bool StopTracking(int id)
    {
        if (idToTileMap.ContainsKey(id)) idToTileMap[id].RemoveStoredId();

        return idToTileMap.Remove(id);
    }

    public Tile GetTile(int x, int y)
    {
        return IsTileValid(x, y) ? tiles[x, y] : null;
    }

    public Tile GetTile(int id)
    {
        return idToTileMap[id];
    }

    // Attempts to set the grid location of an id in the idToTileMap.
    public bool SetGridLocation(int id, int x, int y)
    {
        if (!idToTileMap.ContainsKey(id)) return false;

        if (!IsTileOccupied(x, y)) return false;

        idToTileMap[id].RemoveStoredId();
        idToTileMap[id] = tiles[x, y];
        tiles[x, y].StoredId = id;

        return true;
    }

    // Returns true if a tile is in the tiles array and is not occupied
    public bool IsTileOccupied(int x, int y)
    {
        return IsTileValid(x, y) && 
               !tiles[x, y].IsOccupied;
    }

    // Returns true if a tile is in the tiles array
    public bool IsTileValid(int x, int y)
    {
        return x < tiles.GetLength(0) && 
               y < tiles.GetLength(1) && 
               x >= 0 && 
               y >= 0 &&
               tiles[x, y] != null;
    }
}
