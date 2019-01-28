using System;
using UnityEngine;

/*
 * Creates a Unity gameObject attached to a parent gameObject.
 * Subscribes a method to an event and will automatically handle subscription/unsubscription when needed.
 *
 * Unsubscription happens when the GameObject is disabled or destroyed. If enabled, it will automatically subscribe again.
 * When the parent gameObject is disabled/enabled/destroyed, this object will be affected as well.
 */
public class Listener : MonoBehaviour
{
    private Action<object, EventArgs> eventListener;
    private string currentEventName;

    // Starts listening to this action. Will override any previous action, unsubscribing it in the process
    public void StartListening(Action<object, EventArgs> action, string eventName)
    {
        eventListener?.Unsubscribe(currentEventName); // Unsubscribe if we already have an event

        eventListener = action;
        currentEventName = eventName;

        name = $"__eventListener ({eventName})"; // Set gameObject.name

        eventListener.Subscribe(eventName);
    }

    private void OnDisable()
    {
        eventListener?.Unsubscribe(currentEventName);
    }

    private void OnEnable()
    {
        eventListener?.Subscribe(currentEventName);
    }

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
