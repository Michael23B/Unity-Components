using UnityEngine;

// TODO this can be refactored so we don't have an additional update loop running.

/**
 * Provides helper functions to move an attached gameObject.
 */
public class MovementController : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    private bool isMoving;
    private float dist;
    private Vector3 target;

    void Update()
    {
        if (!isMoving) return;

        dist = Vector3.Distance(target, transform.position);

        if (dist < 0.1)
        {
            StopMoving();
            return;
        }

        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    public void StartMoving(Vector3 target)
    {
        this.target = target;
        isMoving = true;
    }

    public void SetPositionAndRotation(Transform position)
    {
        transform.position = position.position;
        transform.rotation = position.rotation;
        isMoving = false;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
