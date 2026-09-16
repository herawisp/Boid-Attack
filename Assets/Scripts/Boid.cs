using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SteeringBehaviour))]
[RequireComponent(typeof(SpriteRenderer))]

public class Boid : MonoBehaviour
{

    private SteeringBehaviour _steeringBehaviour;
    private SpriteRenderer _spriteRenderer;

    void Awake() {
        _steeringBehaviour = GetComponent<SteeringBehaviour>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        UpdateSeekTargetPosition();
        LookAtMovingDirection();
        transform.position = _steeringBehaviour.Position;
    }

    void LookAtMovingDirection() {
        Vector2 velocity = _steeringBehaviour.Velocity;
        if (velocity.sqrMagnitude > 0.001f) {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void UpdateSeekTargetPosition() {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        _steeringBehaviour.SeekTargetPosition = new(worldPosition.x, worldPosition.y);
    }
}
