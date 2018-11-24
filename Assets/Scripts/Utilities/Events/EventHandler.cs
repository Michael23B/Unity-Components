using System;
using System.Collections.Generic;

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