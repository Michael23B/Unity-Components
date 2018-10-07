using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public static EventHandler Instance { get; private set; }

    private Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

    private void Awake()
    {
        //Singleton instance ensures we always have a single static access point to this class
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent;

        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}