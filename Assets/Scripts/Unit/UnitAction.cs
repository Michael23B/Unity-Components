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
