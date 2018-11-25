using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private Unit selectedUnit;

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
    }

    private void Start()
    {
        Listener.CreateListener(transform, (sender, e) => TileHoverEvent(((TileEventArgs)e).Tile), Constants.EventNames.TileHovered);
        Listener.CreateListener(transform, (sender, e) => TileClickEvent(((TileEventArgs)e).Tile), Constants.EventNames.TileClicked);
    }

    public void SelectUnit(int unitId)
    {
        selectedUnit = UnitRegistry.Instance.GetUnit(unitId);
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
            SelectUnit(tile.StoredId);
            return;
        }

        //If we have a unit, move it
        if (selectedUnit)
        {
            //If the position of this unit on the grid is successfully set, start moving to its new location.
            if (GridController.Instance.SetPositionOfId(selectedUnit.Id, tile.X, tile.Y))
            {
                selectedUnit.movement.StartMoving(GridController.Instance.GetPositionById(selectedUnit.Id));
            }
        }
    }

    #endregion
}