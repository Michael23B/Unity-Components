using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController Instance { get; private set; }
    
    //Values for grid generation
    [SerializeField] private TileGrid tileGrid;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 5;
    [SerializeField] private Vector2 gridMargin = new Vector2(2.5f, -1.875f);
    [SerializeField] private Tile tilePrefab = null;

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
        //Instantiates a grid with all land tiles
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

        //Loop through neighboring tile coordinates and add them to the current coordinates
        for (int i = 0; i < 6; ++i)
        {
            //TODO not working, figure it out
            //https://www.redblobgames.com/grids/hexagons/
            int[,] neighbors = tile.X % 2 == 0 ? Utilities.EvenHexNeighborCoordinates : Utilities.OddHexNeighborCoordinates;

            Tile t = tileGrid.GetTile(tile.X + neighbors[i, 0], tile.Y + neighbors[i, 1]);
            if (t != null) tiles.Add(t);
        }

        return tiles;
    }

    public bool MoveUnit(Unit unit, Tile tile)
    {
        //If we successfully set our position on the grid, start moving the Unit
        if (tileGrid.SetGridLocation(unit.Id, tile.X, tile.Y))
        {
            unit.movement.StartMoving(tile.GetPositionWithOffset());
        }

        return false;
    }
}