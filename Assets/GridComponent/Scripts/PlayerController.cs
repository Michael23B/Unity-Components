using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GridController gridController;
    private int id;

    private void Start()
    {
        id = gameObject.GetInstanceID();

        gridController = FindObjectOfType<GridController>();
        gridController.AddUnitToPositionMap(id, 0, 0);

        TrySetPosition(gridController.GetUnitsPosition(id));
    }

    private void Update()
    {
        HandleInput();
    }

    private void OnDestroy()
    {
        gridController.RemoveUnitFromPositionMap(id);
    }

    //Tries to move this unit on the grid and returns whether it was successful
    public bool GridMoveByAmount(int horizontal, int vertical)
    {
        Vector3? newPos = gridController.GetUnitsNewPosition(id, horizontal, vertical);

        return TrySetPosition(newPos);
    }

    private bool TrySetPosition(Vector3? position)
    {
        if (position != null)
        {
            gameObject.transform.position = (Vector3) position;
            return true;
        }

        return false;
    }

    private void HandleInput()
    {
        //Handle grid movement with keys
        int horizontalMoveAmount = 0;
        int verticalMoveAmount = 0;

        if (Input.GetKeyDown(KeyCode.W)) verticalMoveAmount++;
        if (Input.GetKeyDown(KeyCode.S)) verticalMoveAmount--;
        if (Input.GetKeyDown(KeyCode.D)) horizontalMoveAmount++;
        if (Input.GetKeyDown(KeyCode.A)) horizontalMoveAmount--;

        if (horizontalMoveAmount != 0 || verticalMoveAmount != 0)
            GridMoveByAmount(horizontalMoveAmount, verticalMoveAmount);
    }
}