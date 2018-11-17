using System;
using System.Collections.Generic;

public static class EventHandler
{
    private static readonly Dictionary<string, Action<object, EventArgs>> EventDictionary = new Dictionary<string, Action<object, EventArgs>>();

    public static void StartListening(this Action<object, EventArgs> listener, string eventName)
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

    public static void StopListening(this Action<object, EventArgs> listener, string eventName)
    {
        if (EventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent -= listener;
            EventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, object sender = null, EventArgs e = null)
    {
        if (EventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(sender, e);
        }
    }
}