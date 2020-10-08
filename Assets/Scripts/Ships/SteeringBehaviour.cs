using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class SteeringBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D m_objectToMove;
    public float m_maxSpeed = 5.0f;
    public float m_turnSpeed = 1.0f;
    public float m_wanderCircleDistance = 20.0f;
    public float m_wanderCircleRadius = 5.0f;
    public float m_wanderAngleAdjustment = 0.5f;

    public LayerMask m_avoidThese;
    public float m_avoidDistance = 3.0f;

    private float m_wanderAngle;
    private Vector2 m_steering = Vector2.zero;

    public void Reset()
    {
        m_steering = Vector2.zero;
    }

    public void Seek(Vector3 position, float slowingRadius = 0.0f)
    {
        m_steering += DoSeek(position, slowingRadius);
    }

    public void Flee(Vector2 position)
    {
        m_steering += DoFlee(position);
    }

    public void Wander()
    {
        m_steering += DoWander();
    }

    public void AvoidCollisions()
    {
        m_steering += DoAvoidCollisions(m_avoidDistance);
    }

    // Seek and Arrive combined. Use slowing radius 0 to do regular seek.
    private Vector2 DoSeek(Vector3 position, float slowingRadius)
    {
        // calculate a vector in the direction of the target
        Vector3 desiredVelocity = position - transform.position;
        // calculate the distance to the target
        float distance = desiredVelocity.magnitude;

        // check if the ship needs to slow down
        if (distance < slowingRadius)
        {
            // reduce speed based relative to proximity (closer = slower)
            desiredVelocity = desiredVelocity.normalized * m_maxSpeed * (distance / slowingRadius);
        }
        else
        {
            // outside slowing radius, so go full speed
            desiredVelocity = desiredVelocity.normalized * m_maxSpeed;
        }
        return desiredVelocity - (Vector3)m_objectToMove.velocity;
    }

    // flee from the target
    private Vector2 DoFlee(Vector3 position)
    {
        // calculate a vector away from the target
        Vector3 desiredVelocity = transform.position - position;
        // calculate the distance from the target
        float distance = desiredVelocity.magnitude;
        // change speed to flee the target
        desiredVelocity = desiredVelocity.normalized * m_maxSpeed;

        return desiredVelocity - (Vector3)m_objectToMove.velocity;
    }

    // wander aimlessly
    private Vector2 DoWander()
    {
        // Calculate the wander circle center point.
        Vector2 circleCenter = m_objectToMove.velocity.normalized * m_wanderCircleDistance;
        // Create a displacement vector in local space
        Vector2 displacement = new Vector2(0, m_wanderCircleRadius);
        // Transform the vector by the desired wander angle.
        displacement = SetAngle(displacement, m_wanderAngle);
        // Randomly change the wander angle for the next direction update
        m_wanderAngle += Random.Range(-1f, 1f) * m_wanderAngleAdjustment;

        return circleCenter + displacement;
    }

    private Vector2 SetAngle(Vector2 vector, float rotation)
    {
        float length = vector.magnitude;
        vector.x = Mathf.Cos(rotation) * length;
        vector.y = Mathf.Sin(rotation) * length;
        return vector;
    }

    public void ApplySteering()
    {
        // Limit the turning distance to the turning max speed
        if (m_steering.magnitude > m_turnSpeed)
        {
            m_steering = m_steering.normalized * m_turnSpeed;
        }

        Vector2 velocity = m_objectToMove.velocity + m_steering;
        // Limit velocity to max speed (after adjusting course)
        if (velocity.magnitude > m_maxSpeed)
        {
            velocity = velocity.normalized * m_maxSpeed;
        }

        // Set velocity to new direction and speed
        m_objectToMove.velocity = velocity;
        // Rotate object toward its movement direction
        m_objectToMove.transform.rotation = Quaternion.LookRotation(Vector3.forward, m_objectToMove.velocity);
    }

    private Vector2 DoAvoidCollisions(float distance)
    {
        // move the raycast out of the hull of the ship
        Vector2 originPoint = transform.position + transform.up * 1.75f;
        RaycastHit2D hit = Physics2D.Raycast(originPoint, m_objectToMove.velocity, distance, m_avoidThese);

        // check if the raycast hit something we're trying to avoid
        if (hit)
        {
            // create a steering vector to avoid the collision
            Vector3 desiredVelocity = (hit.transform.position - transform.position).normalized * m_maxSpeed;
            return desiredVelocity - (Vector3)m_objectToMove.velocity;
        }
        return Vector2.zero;
    }
}
