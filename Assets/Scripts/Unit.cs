using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Id { get; private set; }
    private bool initialized = false;

    //TODO refactor MovementController
    public MovementController movement;

    private void Awake()
    {
        Id = GetInstanceID();
        movement = GetComponent<MovementController>();
    }

    //Attempts to spawn a unit and place it on the grid. Optionally allows a manual override for the id (this will be useful for networking).
    public bool Setup(int x, int y, int id = -1)
    {
        if (initialized) return false;

        if (id != -1) Id = id;

        if (!GridController.Instance.StartTrackingId(Id, x, y))
        {
            Debug.LogError($"A unit with the id {Id}({GetInstanceID()}) failed to spawn.");
            return false;
        }

        transform.position = GridController.Instance.GetPositionById(Id);
        initialized = true;

        return true;
    }

    private void OnDestroy()
    {
        GridController.Instance.StopTrackingId(Id);
    }
}
