using UnityEngine;
using UnityEngine.UIElements;

public class SteeringBehaviour : MonoBehaviour
{
    
    public Vector2 Position = Vector2.zero;
    public Vector2 Velocity = Vector2.zero;
    public Vector2 SteeringForce = Vector2.zero;
    public float MaxSpeed = 1;
    public float MaxForce = 0.1f;

    void Update() {
        Velocity += SteeringForce;
        Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);
        Position += Velocity;

        SteeringForce = Vector2.zero;
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
