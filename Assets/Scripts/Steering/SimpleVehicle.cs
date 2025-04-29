using UnityEngine;

public class SimpleVehicle : MonoBehaviour
{
    [SerializeField] private float mass = 1.0f;
    [SerializeField] private float maxForce = 0.01f;
    [SerializeField] public float maxSpeed = 2.0f;

    private Vector3 acceleration;

    public float Mass { get => Mathf.Max(0.001f, mass); }
    public float MaxForce { get => maxForce; }
    public float MaxSpeed { get => maxSpeed; }

    public Vector3 Position { get => transform.position; }
    public Vector3 CurrentVelocity { get; private set; }
    public Vector3 TurnVelocity { get; private set; } // orientation

    public Vector3 ForwardHeading
    { 
        get => transform.up; 
        private set { transform.up = value; } 
    }
    
    // Instantly teleport the vehicle to a different position
    public void Teleport(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    public void Steer(Vector3 desiredVelocity)
    {
        Vector3 force = CalculateSteeringForce(desiredVelocity);
        acceleration += force / Mass;
    }

    public void Stop()
    {
        CurrentVelocity = Vector3.zero;
        acceleration = Vector3.zero;
    }

    private Vector3 CalculateSteeringForce(Vector3 desiredVelocity)
    {
        var steerVector = desiredVelocity - CurrentVelocity;
        var clampedForce = Vector3.ClampMagnitude(steerVector, maxForce);
        return clampedForce;
    }

    private void ApplyMove()
    {
        CurrentVelocity = Vector3.ClampMagnitude(CurrentVelocity + acceleration, MaxSpeed);
        acceleration *= 0;

        transform.Translate(CurrentVelocity * Time.deltaTime, Space.World);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30,30), Mathf.Clamp(transform.position.y, -30, 30), transform.position.z);
    }

    private void AdjustOrientation()
    {
        float speed = CurrentVelocity.magnitude;
        // Ternary operator
        // variable = condition ? <value when true> : <value when false>;
        TurnVelocity = Mathf.Approximately(speed, 0.0f) ? Vector3.zero : CurrentVelocity / speed;

        ForwardHeading = Mathf.Approximately(TurnVelocity.magnitude, 0.0f) ?
            ForwardHeading : TurnVelocity;
    }


    private void Update()
    {
        ApplyMove();
        //AdjustOrientation();
    }
}