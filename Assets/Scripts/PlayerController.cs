using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private MovementController movement;
    private int selectedUnitId;
    public Unit selectedUnit;

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
    }

    private void Start()
    {
        Listener.CreateListener(transform, (sender, e) => TileHoverEvent(((TileEventArgs)e).Tile), Constants.EventNames.TileHovered);
        Listener.CreateListener(transform, (sender, e) => TileClickEvent(((TileEventArgs)e).Tile), Constants.EventNames.TileClicked);

        //TODO remove this later
        selectedUnit.Setup(0, 0);
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
        if (tile.IsOccupied)
        {
            selectedUnitId = tile.StoredId;
        }

        //If the position of this unit on the grid is successfully set, start moving to its new location.
        if (GridController.Instance.SetPositionOfId(selectedUnit.Id, tile.X, tile.Y))
        {
            selectedUnit.movement.StartMoving(GridController.Instance.GetPositionById(selectedUnit.Id));
        }
    }

    #endregion
}