using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private MovementController movement;
    private int selectedUnitId;
    //TODO after movement refactor, we dont need to track entire unit object - only id
    public Unit selectedUnit;

    private void Awake()
    {
        //Singleton instance ensures we always have a single static access point to this class
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        EventHandler.Instance.StartListening(Constants.EventNames.TileHovered, (sender, e) => TileHoverEvent(((TileEventArgs)e).Tile));
        EventHandler.Instance.StartListening(Constants.EventNames.TileClicked, (sender, e) => TileClickEvent(((TileEventArgs)e).Tile));

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
        //Debug.Log("Hover event called");
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