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

    [Header("Seek Behaviour")]
    public Vector2 SeekTargetPosition;
    
    [Header("Flee Behaviour")]
    public Vector2 FleeTargetPosition;

    void Update() {
        if (SeekForceWeight != 0) ApplyForce(SeekForce(SeekTargetPosition.x, SeekTargetPosition.y), SeekForceWeight);
        if (FleeForceWeight != 0) ApplyForce(FleeForce(FleeTargetPosition.x, FleeTargetPosition.y), FleeForceWeight);

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
}
