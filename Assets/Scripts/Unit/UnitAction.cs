public class UnitAction
{
    public UnitAction() {}
    public UnitAction(Tile target, string name)
    {
        Target = target;
        Name = name;
    }

    public Tile Target { get; set; }
    public string Name { get; set; } = "";
}
