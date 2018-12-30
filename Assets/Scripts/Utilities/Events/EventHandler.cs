using System;
using System.Collections.Generic;

/*
 * Generic event handler. Stores events in a dictionary and invokes them when Invoke() is called.
 * Subscribe a function to a string and when Invoke() is called with that string, the function will be called.
 *
 * Use Constants.EventName to subscribe to or invoke events rather than raw strings.
 * Remember to Unsubscribe() before an object is destroyed.
 * Remember to keep a reference to the listener if you need to unsubscribe at some point.
 *
 * A related class, Listener, can be used to create a Unity gameObject with a subscription that will handle its own subscribe/unsubscribe calls when needed.
 */
public static class EventHandler
{
    private static readonly Dictionary<string, Action<object, EventArgs>> EventDictionary = new Dictionary<string, Action<object, EventArgs>>();

    public static void Subscribe(this Action<object, EventArgs> listener, string eventName)
    {
        if (EventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent += listener;
            EventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            EventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void Unsubscribe(this Action<object, EventArgs> listener, string eventName)
    {
        if (EventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent -= listener;
            if (thisEvent == null) EventDictionary.Remove(eventName);
            else EventDictionary[eventName] = thisEvent;
        }
    }

    public static void Invoke(string eventName, object sender = null, EventArgs e = null)
    {
        if (EventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(sender, e);
        }
    }
}