using System;

/*
 * Listens for lifecycle events and sets static fields for other classes to access.
 */
public class GameState
{
    public GameState()
    {
        EventHandler.Subscribe((sender, e) => IsApplicationQuitting = true, Constants.EventName.APPLICATIONQUITTING);
        // TODO on turn start event -> activeUnit === unit
    }

    private Action<object, EventArgs> eventListener;

    public bool IsApplicationQuitting { get; private set; } = false;
}
