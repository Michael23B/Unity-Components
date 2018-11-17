using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private MovementController movement;
    private int selectedUnitId;
    public Unit selectedUnit;

    private Action<object, EventArgs> tileHoveredListener;
    private Action<object, EventArgs> tileClickedListener;

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
    }

    private void Start()
    {
        tileHoveredListener = (sender, e) => TileHoverEvent(((TileEventArgs) e).Tile);
        tileClickedListener = (sender, e) => TileClickEvent(((TileEventArgs) e).Tile);

        tileHoveredListener.StartListening(Constants.EventNames.TileHovered);
        tileClickedListener.StartListening(Constants.EventNames.TileClicked);

        //TODO remove this later
        selectedUnit.Setup(0, 0);
    }

    private void OnDestroy()
    {
        tileHoveredListener.StopListening(Constants.EventNames.TileHovered);
        tileClickedListener.StopListening(Constants.EventNames.TileClicked);
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
        //If the position of this unit on the grid is successfully set, start moving to its new location.
        if (GridController.Instance.SetPositionOfId(selectedUnit.Id, tile.X, tile.Y))
        {
            selectedUnit.movement.StartMoving(GridController.Instance.GetPositionById(selectedUnit.Id));
        }
    }

    #endregion
}