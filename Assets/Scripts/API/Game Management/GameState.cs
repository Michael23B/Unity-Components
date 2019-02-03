using System;

/*
 * Listens for lifecycle events and sets static fields for other classes to access.
 */
public class GameState
{
    public GameState()
    {
        EventHandler.Subscribe((sender, e) => IsApplicationQuitting = true, Constants.EventName.APPLICATIONQUITTING);
        EventHandler.Subscribe((sender, e) => ActiveUnit = ((TurnEventArgs)e).CurrentUnit, Constants.EventName.UNITTURNSTARTED);
    }

    private Action<object, EventArgs> eventListener;

    public bool IsApplicationQuitting { get; private set; } = false;
    public Unit ActiveUnit { get; private set; } = null;
}
