using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float DestroyAfterSeconds = 2f;

    private void Awake()
    {
        Destroy(gameObject, DestroyAfterSeconds);
    }
}
