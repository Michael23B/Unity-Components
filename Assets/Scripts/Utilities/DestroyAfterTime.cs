using UnityEngine;

/*
 * Destroys an attached gameObject after the provided time.
 */
public class DestroyAfterTime : MonoBehaviour
{
    public float DestroyAfterSeconds = 2f;

    private void Awake()
    {
        Destroy(gameObject, DestroyAfterSeconds);
    }
}
