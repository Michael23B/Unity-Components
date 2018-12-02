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
        Listener.CreateListener(transform, (sender, e) => TileHoverEvent(((TileEventArgs)e).Tile), Constants.EventNames.TILEHOVERED);
        Listener.CreateListener(transform, (sender, e) => TileClickEvent(((TileEventArgs)e).Tile), Constants.EventNames.TILECLICKED);
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
            GridController.Instance.MoveUnit(selectedUnit, tile);
            return;
        }
    }

    #endregion
}