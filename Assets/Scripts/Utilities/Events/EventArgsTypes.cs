﻿using System;

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