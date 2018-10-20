using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    private bool isMoving;
    private float dist;
    private Vector3 target;
    private Transform startPos;

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
        startPos = transform;
        this.target = target;
        isMoving = true;
    }

    public void UndoMovement()
    {
        transform.position = startPos.position;
        transform.rotation = startPos.rotation;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
