using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SteeringBehaviour : MonoBehaviour
{
    
    public Vector2 Position = Vector2.zero;
    public Vector2 Velocity = Vector2.zero;
    public Vector2 SteeringForce = Vector2.zero;
    public float MaxSpeed = 1;
    public float MaxForce = 0.1f;

    [Header("Behaviour Weights")]
    public float SeekForceWeight;
    public float FleeForceWeight;
    public float PursueForceWeight;
    public float EvadeForceWeight;
    public float ArriveForceWeight;

    [Header("Seek Behaviour")]
    public Vector2 SeekTargetPosition;
    
    [Header("Flee Behaviour")]
    public Vector2 FleeTargetPosition;
    
    [Header("Pursue Behaviour")]
    public SteeringBehaviour PursuedAgent;
    
    [Header("Evade Behaviour")]
    public SteeringBehaviour EvadeAgent;
    
    [Header("Arrive Behaviour")]
    public Vector2 ArriveTargetPosition;
    public float SlowingRadius;

    [Header("Wander Behaviour")]
    public float WanderDistance;
    public float WanderPower;
    public float WanderAngle;
    public float WanderChange;

    void Update() {
        if (SeekForceWeight != 0) ApplyForce(SeekForce(SeekTargetPosition.x, SeekTargetPosition.y), SeekForceWeight);
        if (FleeForceWeight != 0) ApplyForce(FleeForce(FleeTargetPosition.x, FleeTargetPosition.y), FleeForceWeight);
        if (PursueForceWeight != 0) ApplyForce(PursueForce(PursuedAgent), PursueForceWeight);
        if (EvadeForceWeight != 0) ApplyForce(EvadeForce(EvadeAgent), EvadeForceWeight);
        if (ArriveForceWeight != 0) ApplyForce(ArriveForce(ArriveTargetPosition.x, ArriveTargetPosition.y, SlowingRadius), ArriveForceWeight);


        Velocity += SteeringForce;
        Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);
        Position += Velocity;

        SteeringForce = Vector2.zero;
    }

    private void ApplyForce(Vector2 force, float weight) {
        force *= weight;
        SteeringForce += force;
    }

    private Vector2 SeekForce(float x, float y) {
        Vector2 vector = new(x, y);
        vector -= Position;
        vector = vector.normalized * MaxSpeed;
        vector -= Velocity;
        vector = Vector2.ClampMagnitude(vector, MaxForce);
        return vector;
    }

    private Vector2 FleeForce(float x, float y) {
        Vector2 vector = new(x, y);
        vector -= Position;
        vector = vector.normalized * MaxSpeed;
        vector *= -1;
        vector -= Velocity;
        vector = Vector2.ClampMagnitude(vector, MaxForce);
        return vector;
    }

    private Vector2 PursueForce(SteeringBehaviour other) {
        Vector2 vector = other.Velocity;
        vector *= 10;
        vector += other.Position;
        return SeekForce(vector.x, vector.y);
    }

    private Vector2 EvadeForce(SteeringBehaviour other) {
        Vector2 vector = other.Velocity;
        vector *= 10;
        vector += other.Position;
        return FleeForce(vector.x, vector.y);
    }

    private Vector2 ArriveForce(float x, float y, float slowingRadius) {
        Vector2 vector = new(x, y);
        vector -= Position;

        float dist = vector.magnitude;
        if (dist > slowingRadius) {
            vector = vector.normalized * MaxSpeed;
        } else {
            vector = vector.normalized * (dist / slowingRadius);
        }

        vector -= Velocity;
        vector = Vector2.ClampMagnitude(vector, MaxForce);
        return vector;
    }

    // private Vector2 WanderForce() {
    //     Vector2 vector = Velocity;
    //     vector = vector.normalized * WanderDistance;
        
    //     float radians = ()
    // }
}
