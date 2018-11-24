using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private MovementController movement;
    private int selectedUnitId;
    private Unit selectedUnit;
    //TODO remove later, find some place to put it
    public GameObject unitPrefab;

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
    }

    private void Start()
    {
        Listener.CreateListener(transform, (sender, e) => TileHoverEvent(((TileEventArgs)e).Tile), Constants.EventNames.TileHovered);
        Listener.CreateListener(transform, (sender, e) => TileClickEvent(((TileEventArgs)e).Tile), Constants.EventNames.TileClicked);

        //TODO remove later
        selectedUnit = Unit.CreateAndRegisterUnit(unitPrefab, 0, 0);
        Unit.CreateAndRegisterUnit(unitPrefab, 1, 0);
    }

    public void SelectUnit(int unitId)
    {
        selectedUnitId = unitId;
    }

    #region Events

    private void TileHoverEvent(Tile tile)
    {
        if (tile.IsOccupied) Debug.Log(tile.StoredId);
    }

    private void TileClickEvent(Tile tile)
    {
        //If the tile is occupied, select the unit stored in that tile
        if (tile.IsOccupied)
        {
            selectedUnitId = tile.StoredId;
            selectedUnit = UnitRegistry.Instance.GetUnit(selectedUnitId);
            return;
        }

        //If the position of this unit on the grid is successfully set, start moving to its new location.
        if (GridController.Instance.SetPositionOfId(selectedUnit.Id, tile.X, tile.Y))
        {
            selectedUnit.movement.StartMoving(GridController.Instance.GetPositionById(selectedUnit.Id));
        }
    }

    #endregion
}