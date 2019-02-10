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
        Stats = new UnitStats();
    }

    public void OverrideId(int id)
    {
        Id = id;
    }

    public void OverrideStats(UnitStats stats)
    {
        Stats = stats;
    }

    public void Act(UnitAction action)
    {
        switch (action.ActionType)
        {
            case Constants.ActionType.MOVE:

                // If we successfully update our position on the grid, move to that tile
                if (GameComponents.GridController.MoveUnit(this, action.Target))
                {
                    Movement.StartMoving(action.Target.GetPositionWithOffset());
                    Debug.Log($"{Id} moved");
                }

                break;
            case Constants.ActionType.ATTACK:
                
                Unit target = GameComponents.UnitRegistry.GetUnit(action.Target.StoredId);
                if (target)
                {
                    Attack(target);
                    Debug.Log($"{Id} attacked");
                }

                break;
            default:
                break;
        }
    }

    public void Damage(int amount)
    {
        Stats.Health -= amount;

        if (Stats.Health <= 0)
        {
            Die();
        }
    }

    private void Attack(Unit target)
    {
        target.Damage(1);
    }

    private void Die()
    {
        Debug.Log($"{Id} died");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventHandler.Invoke(Constants.EventName.UNITDESTROYED, this, new UnitEventArgs(this));
    }
}
