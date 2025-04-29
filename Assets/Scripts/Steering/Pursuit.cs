using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : MonoBehaviour
{
    private SimpleVehicle vehicle;

    private SimpleVehicle pursuitTarget;
    private SpriteRenderer sprite;
    private Vector3 desiredVelocity;
    // Start is called before the first frame update

    private void Awake()
    {
        vehicle = GetComponent<SimpleVehicle>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Move(GameObject target)
    {
        if (desiredVelocity.x < 0.0f)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        pursuitTarget = target.GetComponent<SimpleVehicle>();
        vehicle.Steer(Calculate());
    }

    public void StopPrusuit()
    {
        vehicle.Stop();
    }
    private Vector3 Calculate()
    {
        if (pursuitTarget == null) return Vector3.zero;

        Vector3 targetDirection = (pursuitTarget.Position - vehicle.Position).normalized;

        Vector3 targetMovementDirection = pursuitTarget.CurrentVelocity.normalized;

        float dot = Vector3.Dot(targetDirection, targetMovementDirection);

        Vector3 predicatedTargetLocation = pursuitTarget.Position;

        if (dot >= -0.9239f)
        {
            predicatedTargetLocation += Prediction(pursuitTarget);
        }

        Vector3 direction = (predicatedTargetLocation - vehicle.Position).normalized;

        direction = new Vector3(direction.x, direction.y, 0);

        desiredVelocity = direction * vehicle.MaxSpeed;
        return desiredVelocity;


    }

    private Vector3 Prediction(SimpleVehicle target)
    {
        //return Vector3.zero;
        //return target.CurrentVelocity * predictAheadTime;
        float distanceToTarget = (target.Position - vehicle.Position).magnitude;
        float time = distanceToTarget / vehicle.MaxSpeed;
        return target.CurrentVelocity * time;
    }
}
