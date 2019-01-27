using UnityEngine;

/*
 * Stores data and unit-related classes for other classes to access.
 * Units are created using the static class UnitCreation.
 */
[RequireComponent(typeof(MovementController))]
public class Unit : MonoBehaviour
{
    public int Id { get; private set; }
    public MovementController Movement { get; private set; }
    public UnitStats Stats { get; private set; }
    [SerializeField] public Constants.UnitPrefabType UnitPrefabType; // Used to identify resource (ResouceManager)

    private void Awake()
    {
        Id = GetInstanceID();
        Movement = GetComponent<MovementController>();
        Stats = new UnitStats(10);
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
