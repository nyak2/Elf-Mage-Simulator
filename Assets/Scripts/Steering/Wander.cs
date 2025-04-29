using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SimpleVehicle))]
public class Wander : MonoBehaviour
{
    private SimpleVehicle vehicle;

    private Vector3 desiredVelocity;
    private Vector3 randomPoint;

    private bool isWandering;

    public float wanderRadius = 2.0f;
    public float distanceAhead = 3.0f;
    public float wanderInterval;

    private SpriteRenderer sprite;
    private void Awake()
    {
        vehicle = GetComponent<SimpleVehicle>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void StartWandering()
    {
        if(desiredVelocity.x < 0.0f)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        vehicle.Steer(Calculate());

        if (!isWandering)
        {
            StartCoroutine(wander());
        }
    }

    public void StopWandering()
    {
        StopAllCoroutines();
        isWandering = false;
        vehicle.Stop();
    }    
    private IEnumerator wander()
    {
        isWandering = true;

        Vector3 dir = Random.insideUnitCircle ;
        Vector3 circleCenter = vehicle.Position + (dir * wanderRadius * distanceAhead);
        float angle = Mathf.Deg2Rad * Random.Range(0, 360);
        randomPoint = circleCenter + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * wanderRadius);
        yield return new WaitForSeconds(wanderInterval);

        isWandering = false;
    }

    private Vector3 Calculate()
    {
        if (randomPoint == Vector3.zero)
        {
            return Vector3.zero;
        }

        Vector3 direction = randomPoint - vehicle.Position;
        direction = new Vector3(direction.x, direction.y, 0);
        float speedToUse;
        if(direction.magnitude <= 0.01f)
        {
            speedToUse = 0.0f;
        }
        else
        {
            speedToUse = vehicle.MaxSpeed;
        }

        desiredVelocity = direction.normalized * speedToUse;
        return desiredVelocity;
    }
}
