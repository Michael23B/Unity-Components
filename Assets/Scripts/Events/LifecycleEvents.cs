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

    private void OnApplicationQuit()
    {
        EventHandler.Invoke(Constants.EventName.APPLICATIONQUITTING);
    }
}
