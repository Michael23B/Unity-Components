using System;


public class TileEventArgs : EventArgs
{
    public TileEventArgs(Tile tile)
    {
        Tile = tile;
    }
    public Tile Tile { get; }
}