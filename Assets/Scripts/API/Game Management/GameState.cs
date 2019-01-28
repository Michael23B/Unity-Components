using System;

/*
 * Listens for lifecycle events and sets static fields for other classes to access.
 */
public class GameState
{
    public void Initialize()
    {
        EventHandler.Subscribe((sender, e) => IsApplicationQuitting = true, Constants.EventName.APPLICATIONQUITTING);
    }

    private Action<object, EventArgs> eventListener;

    public bool IsApplicationQuitting { get; private set; } = false;
}
