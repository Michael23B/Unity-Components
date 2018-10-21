using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class EventHandler : MonoBehaviour
{
    public static EventHandler Instance { get; private set; }

    private Dictionary<string, Action<Object, EventArgs>> eventDictionary = new Dictionary<string, Action<Object, EventArgs>>();

    private void Awake()
    {
        //Singleton instance ensures we always have a single static access point to this class
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void StartListening(string eventName, Action<Object, EventArgs> listener)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent += listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, Action<Object, EventArgs> listener)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent -= listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public void TriggerEvent(string eventName, Object sender = null, EventArgs e = null)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(sender, e);
        }
    }
}