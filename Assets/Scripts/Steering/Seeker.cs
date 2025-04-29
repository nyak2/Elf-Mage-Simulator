using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SimpleVehicle))]
public class Seeker : MonoBehaviour
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

    public void StopSeek()
    {
        vehicle.Stop();
    }

    private Vector3 Seek(Vector3 target)
    {

        Vector3 displacement = target - vehicle.Position;
        Vector3 targetDirection = displacement.normalized;
        targetDirection = new Vector3(targetDirection.x, targetDirection.y, 0);
        float speedToUse;

        float distance = displacement.magnitude;

        if (distance <= stoppingDistance)
        {
            hasReached = true;
            speedToUse = 0.0f;
        }
        else
        {
            hasReached = false;
            float rampedSpeed = vehicle.MaxSpeed * (distance / slowingRadius);
            speedToUse = Mathf.Min(rampedSpeed, vehicle.MaxSpeed);
        }

        desiredVelocity = targetDirection * speedToUse;

        return desiredVelocity;
    }
}