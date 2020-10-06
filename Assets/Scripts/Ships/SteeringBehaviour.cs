using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class SteeringBehaviour : MonoBehaviour
{
    Rigidbody2D objectToMove;
    Transform objectTransform;
    public float maxSpeed = 5.0f;
    public float turnSpeed = 0.5f;
    public float wanderCircleDistance = 20.0f;
    public float wanderCircleRadius = 5.0f;
    public float wanderAngleAdjustment = 0.5f;

    public LayerMask avoidThese;
    public float avoidDistance = 3.0f;
    public float avoidTurningSpeed = 2.0f;

    private bool collisionImminent = false;
    private float wanderAngle;
    private Vector2 turnToAvoid = Vector2.zero;
    private Vector2 steering = Vector2.zero;

    void Start()
    {
        objectToMove = GetComponent<Rigidbody2D>();
        objectTransform = objectToMove.gameObject.transform;
        objectToMove.gravityScale = 0.0f;
    }

    protected void Update()
    {
        // we need the object's up vector for our 2D forward direction
        Vector2 forward = objectTransform.up;
        RaycastHit2D[] hits = Physics2D.RaycastAll(objectTransform.position, forward, avoidDistance, avoidThese);
        Debug.DrawRay(objectTransform.position, forward * avoidDistance, Color.red);

        if (hits.Length > 1)
        {
            turnToAvoid = FindAvoidAngle(hits[1].collider.tag);
        }
        else
        {
            turnToAvoid = Vector2.zero;
        }
    }

    public void Chase(Vector2 position, float slowingRadius = 0.0f)
    {
        steering += DoChase(position, slowingRadius);
    }

    private Vector2 DoChase(Vector3 position, float slowingRadius)
    {
        // calculate a vector in the direction of the target
        Vector3 direction = position - transform.position;
        // calculate the distance to the target
        float distance = direction.magnitude;

        Vector2 velocity;
        // check if the ship needs to slow down
        if (distance < slowingRadius)
        {
            // reduce speed based relative to proximity (closer = slower)
            velocity = direction.normalized * maxSpeed * (distance / slowingRadius);
        }
        else
        {
            // outside slowing radius, so go full speed
            velocity = direction.normalized * maxSpeed;
        }
        return velocity - objectToMove.velocity;
    }

    public void Wander()
    {
        steering += DoWander();
    }

    private Vector2 DoWander()
    {
        // Calculate the wander circle center point.
        Vector3 circleCenter = objectToMove.velocity.normalized * wanderCircleDistance;
        // Create a displacement vector in local space
        Vector3 displacement = new Vector3(0, wanderCircleRadius, 0);
        // Transform the vector by the desired wander angle.
        displacement = SetAngle(displacement, wanderAngle);
        // Randomly change the wander angle for the next direction update
        wanderAngle += Random.Range(-1f, 1f) * wanderAngleAdjustment;

        return circleCenter + displacement;
    }

    public void Flee(Vector2 position, float fleeRadius)
    {
        steering += DoFlee(position, fleeRadius);
    }

    private Vector2 DoFlee(Vector3 position, float fleeRadius)
    {
        Vector2 direction = position - transform.position;
        float distance = direction.magnitude;

        Vector2 velocity;
        if (distance < fleeRadius)
        {
            velocity = -direction.normalized * maxSpeed;
        }
        else
        {
            velocity = DoWander();
        }
        return velocity - objectToMove.velocity;
    }

    private Vector2 SetAngle(Vector3 vector, float rotation)
    {
        float length = vector.magnitude;
        vector.x = Mathf.Cos(rotation) * length;
        vector.y = Mathf.Sin(rotation) * length;
        return vector;
    }

    private Vector2 FindAvoidAngle(string boundary)
    {
        float dot = Vector2.Dot(objectToMove.velocity, Vector2.left);
        Vector2 turnDirection;

        if (dot > 0)
        {
            if (boundary == "LeftBoundary")
            {
                if (objectTransform.rotation.eulerAngles.z < 90.0f)
                {
                    turnDirection = objectToMove.velocity.normalized + Vector2.up;
                }
                else
                {
                    turnDirection = objectToMove.velocity.normalized + Vector2.down;
                }
            }
            else
            {
                turnDirection = objectToMove.velocity.normalized + Vector2.left;
            }
        }
        else
        {
            if (boundary == "RightBoundary")
            {
                if (objectTransform.rotation.eulerAngles.z < 270.0f)
                {
                    turnDirection = objectToMove.velocity.normalized + Vector2.down;
                }
                else
                {
                    turnDirection = objectToMove.velocity.normalized + Vector2.up;
                }
            }
             else
            {
                turnDirection = objectToMove.velocity.normalized + Vector2.right;
            }
        }

        return turnDirection * avoidTurningSpeed;
    }

    public void ApplySteering()
    {

        // Limit the turning distance to the turning max speed
        if (steering.magnitude > turnSpeed)
        {
            steering = steering.normalized * turnSpeed;
        }
        steering += turnToAvoid;
        Vector2 velocity = objectToMove.velocity + steering;

        // Limit velocity to max speed (after adjusting course)
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        // Set velocity to new direction and speed
        objectToMove.velocity = velocity;
        // Rotate object toward its movement direction
        objectToMove.transform.rotation = Quaternion.LookRotation(Vector3.forward, velocity);
    }
}
