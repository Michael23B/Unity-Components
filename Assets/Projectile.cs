using UnityEngine;

public class Projectile
{
    private readonly GameObject gameObject;
    private readonly MovementController movement;
    private readonly GameObject projectile;

    public Projectile(GameObject projectilePrefab, Transform startPos, Transform parent, Vector3 target, float moveSpeed)
    {
        gameObject = new GameObject("__projectile", typeof(MovementController));
        gameObject.transform.SetParent(parent);
        gameObject.transform.SetPositionAndRotation(startPos.position, startPos.rotation);

        projectile = Object.Instantiate(projectilePrefab);

        movement = gameObject.GetComponent<MovementController>();
        movement.moveSpeed = moveSpeed;
        movement.AddOnTargetReached(OnTargetReached);
        movement.StartMoving(target);
    }

    private void OnTargetReached()
    {
        Object.Destroy(projectile);
        Object.Destroy(gameObject);
    }
}
