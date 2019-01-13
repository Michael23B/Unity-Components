using UnityEngine;

/*
 * Stores data and unit-related classes for other classes to access.
 * Units are created using the static class UnitCreation.
 */
public class Unit : MonoBehaviour
{
    public int Id { get; private set; }

    public MovementController movement;
    public UnitStats stats;

    private void Awake()
    {
        Id = GetInstanceID();
        movement = GetComponent<MovementController>();
        stats = new UnitStats(10);
    }

    public void OverrideId(int id)
    {
        Id = id;
    }

    private void OnDestroy()
    {
        EventHandler.Invoke(Constants.EventName.UNITDESTROYED, this, new UnitEventArgs(this));
    }
}
