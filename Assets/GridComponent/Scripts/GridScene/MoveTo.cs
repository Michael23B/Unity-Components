using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    private float dist;
    private Transform target;
    private Transform startPos;

    void Update()
    {
        if (!target) return;

        dist = Vector3.Distance(target.transform.position, transform.position);

        if (dist < 0.1)
        {
            StopMoving();
            return;
        } 

        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    public void StartMoving(Transform target)
    {
        startPos = transform;
        this.target = target;
    }

    public void UndoMovement()
    {
        transform.position = startPos.position;
        transform.rotation = startPos.rotation;
        startPos = null;
    }

    public void StopMoving()
    {
        target = null;
    }
}
