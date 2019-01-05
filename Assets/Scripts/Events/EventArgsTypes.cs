using System;

/*
 * Concrete types of the arguments to be passed from events.
 */

public class TileEventArgs : EventArgs
{
    public TileEventArgs(Tile tile)
    {
        Tile = tile;
    }
    public Tile Tile { get; }
}

public class UnitEventArgs : EventArgs
{
    public UnitEventArgs(Unit unit)
    {
        Unit = unit;
    }
    public Unit Unit { get; }
}

public class TurnEventArgs : EventArgs
{
    public TurnEventArgs(Unit currentUnit, int turn, int roundLength, int turnsTaken)
    {
        CurrentUnit = currentUnit;
        Turn = turn;
        RoundLength = roundLength;
        TurnsTaken = turnsTaken;
    }
    public Unit CurrentUnit { get; }
    public int Turn { get; }
    public int RoundLength { get; }
    public int TurnsTaken { get; }
}