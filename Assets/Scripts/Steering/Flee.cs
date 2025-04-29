using UnityEngine;

[RequireComponent(typeof(SimpleVehicle))]
public class Flee : MonoBehaviour
{
    private SimpleVehicle vehicle;

    public float stoppingDistance = 0.05f;
    public float slowingRadius = 1.0f;
    public bool hasReached = false;

    private SpriteRenderer sprite;
    private Vector3 desiredVelocity;

    private void Awake()
    {
        vehicle = GetComponent<SimpleVehicle>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public bool HasReachedDestination => hasReached;

    public void Move(Vector3 target)
    {
        if (desiredVelocity.x < 0.0f)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        vehicle.Steer(Seek(target));
    }

    public void StopFlee()
    {
        vehicle.Stop();
    }

    private Vector3 Seek(Vector3 target)
    {

        Vector3 displacement = vehicle.Position - target;
        Vector3 targetDirection = displacement.normalized;
        targetDirection = new Vector3(targetDirection.x, targetDirection.y, 0);

        desiredVelocity = targetDirection * vehicle.MaxSpeed;

        return desiredVelocity;
    }
}