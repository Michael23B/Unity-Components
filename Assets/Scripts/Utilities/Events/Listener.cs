using System;
using UnityEngine;

//Listens for an event. Automatically unsubscribes when the GameObject is disabled or destroyed. If enabled will automatically subscribe again.
public class Listener : MonoBehaviour
{
    private Action<object, EventArgs> eventListener;
    private string currentEventName;

    //Starts listening to this action. Will override any previous action, unsubscribing it in the process
    public void StartListening(Action<object, EventArgs> action, string eventName)
    {
        eventListener?.Unsubscribe(currentEventName); //Unsubscribe if we already have an event

        eventListener = action;
        currentEventName = eventName;

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

    public static Listener CreateListener(Transform parent)
    {
        GameObject go = new GameObject("__eventListener", typeof(Listener));
        go.transform.parent = parent;

        return go.GetComponent<Listener>();
    }

    public static Listener CreateListener(Transform parent, Action<object, EventArgs> action, string eventName)
    {
        Listener listener = CreateListener(parent);

        listener.StartListening(action, eventName);

        return listener;
    }
}
