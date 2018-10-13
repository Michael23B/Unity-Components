using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class EventHandler : MonoBehaviour
{
    public static EventHandler Instance { get; private set; }

    private Dictionary<string, GenericEvent> eventDictionary = new Dictionary<string, GenericEvent>();

    private void Awake()
    {
        //Singleton instance ensures we always have a single static access point to this class
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void StartListening(string eventName, UnityAction<Object, EventArgs> listener)
    {
        GenericEvent thisEvent;

        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GenericEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, UnityAction<Object, EventArgs> listener)
    {
        GenericEvent thisEvent;

        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public void TriggerEvent(string eventName, Object sender = null, EventArgs e = null)
    {
        GenericEvent thisEvent;

        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(sender, e);
        }
    }
}