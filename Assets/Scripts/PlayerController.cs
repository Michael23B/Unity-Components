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
        Listener.CreateListener(transform, (sender, e) => TileHoverEvent(((TileEventArgs)e).Tile), Constants.EventName.TILEHOVERED);
        Listener.CreateListener(transform, (sender, e) => TileClickEvent(((TileEventArgs)e).Tile), Constants.EventName.TILECLICKED);
        Listener.CreateListener(transform, (sender, e) => TileRightClickEvent(((TileEventArgs)e).Tile), Constants.EventName.TILERIGHTCLICKED);
        Listener.CreateListener(transform, (sender, e) => UnitDestroyedEvent(((UnitEventArgs)e).Unit), Constants.EventName.UNITDESTROYED);

        TileHoveredParticleEffect.Start(); // TODO move this after testing
    }

    public void SelectUnit(int unitId)
    {
        selectedUnit = UnitRegistry.Instance.GetUnit(unitId);
    }

    public void DeselectUnit()
    {
        selectedUnit = null;
    }

    #region Events

    private void TileHoverEvent(Tile tile)
    {
        //TODO Face the unit in the direction of the tile
    }

    private void UnitDestroyedEvent(Unit unit)
    {
        if (unit.Id == selectedUnit.Id) DeselectUnit();
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

    private void TileRightClickEvent(Tile tile)
    {
        //If we have a unit and the target tile has a different unit, destroy it
        if (selectedUnit && tile.IsOccupied && tile.StoredId != selectedUnit.Id)
        {
            Unit target = UnitRegistry.Instance.GetUnit(tile.StoredId);
            Destroy(target.gameObject);
        }
    }

    #endregion
}