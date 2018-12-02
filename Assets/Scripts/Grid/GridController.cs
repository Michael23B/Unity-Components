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

    public Vector3 GetPosition(Unit unit)
    {
        return tileGrid.GetPositionById(unit.Id);
    }

    public bool MoveUnit(Unit unit, Tile tile)
    {
        if (tileGrid.SetPositionOfId(unit.Id, tile.X, tile.Y))
        {
            unit.movement.StartMoving(tile.GetPositionWithOffset());
        }

        return false;
    }
}