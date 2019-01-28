using UnityEngine;

/*
 * Fires unity lifecycle events using event handler so non-GameObjects can listen for them
 */
public class LifecycleEvents : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // TODO depending on the order of execution, the event handler might be destroyed before this happens
    private void OnApplicationQuit()
    {
        EventHandler.Invoke(Constants.EventName.APPLICATIONQUITTING);
    }

    // TODO add lifecycle events as needed here
}
