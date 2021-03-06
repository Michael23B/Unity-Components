﻿using System.Collections.Generic;
using UnityEngine;

/*
 * Hides the implementation of TileGrid and provides methods for interfacing with it.
 */
public class GridController : MonoBehaviour
{
    // Values for grid generation
    [SerializeField] private TileGrid tileGrid;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 5;
    [SerializeField] private Vector2 gridMargin = new Vector2(2.5f, -1.875f);
    [SerializeField] private Tile tilePrefab = null;

    private void Awake()
    {
        // Instantiates a grid with all land tiles
        tileGrid = new TileGrid(tilePrefab, new int[rows, columns].Initialize2DArray(1), gridMargin, transform);
    }

    public bool StartTracking(Unit unit, int startPositionX, int startPositionY)
    {
        return tileGrid.StartTracking(unit.Id, startPositionX, startPositionY);
    }

    public bool StopTracking(Unit unit)
    {
        return tileGrid.StopTracking(unit.Id);
    }

    public Tile GetTile(Unit unit)
    {
        return tileGrid.GetTile(unit.Id);
    }

    public Vector3 GetPosition(Unit unit)
    {
        return tileGrid.GetTile(unit.Id).GetPositionWithOffset();
    }

    public List<Tile> GetSurroundingTiles(Unit unit)
    {
        Tile t = tileGrid.GetTile(unit.Id);
        return GetSurroundingTiles(t);
    }

    public List<Tile> GetSurroundingTiles(Tile tile)
    {
        if (!tile) return new List<Tile>();

        List<Tile> tiles = new List<Tile>();

        // Loop through neighboring tile coordinates and add them to the current coordinates
        for (int i = 0; i < 6; ++i)
        {
            int[] neighborCoords = HexHelper.GetHexNeighborCoordinates(tile.Y % 2 == 0, i);

            Tile t = tileGrid.GetTile(tile.X + neighborCoords[0], tile.Y + neighborCoords[1]);
            if (t != null) tiles.Add(t);
        }

        return tiles;
    }

    public bool MoveUnit(Unit unit, Tile tile)
    {
        return tileGrid.SetGridLocation(unit.Id, tile.X, tile.Y);
    }
}