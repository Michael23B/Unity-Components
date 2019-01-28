/*
 * Describes an action that a Unit can take.
 * (Not implemented) Stored data is used to decide what action will be taken.
 */
public class UnitAction
{
    public UnitAction() {}
    public UnitAction(Tile target, int actionType)
    {
        Target = target;
        ActionType = actionType;
    }

    public Tile Target { get; set; }
    public int ActionType { get; set; } = -1;
}
