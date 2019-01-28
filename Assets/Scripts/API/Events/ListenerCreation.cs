using System;
using UnityEngine;

public static class ListenerCreation
{
    // Creates an object that allows you to subscribe to an event. This subscription automatically handled when parent is enabled/disabled/destroyed.
    public static Listener CreateListener(Transform parent)
    {
        GameObject go = new GameObject("__eventListener", typeof(Listener));
        go.transform.parent = parent;

        return go.GetComponent<Listener>();
    }

    // Creates an object subscribed to an event. This subscription automatically handled when parent is enabled/disabled/destroyed.
    public static Listener CreateListener(Transform parent, Action<object, EventArgs> action, string eventName)
    {
        Listener listener = CreateListener(parent);

        listener.StartListening(action, eventName);

        return listener;
    }
}
