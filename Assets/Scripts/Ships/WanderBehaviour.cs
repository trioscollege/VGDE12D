using UnityEngine;

public class WanderBehaviour : SteeringBehaviour
{
    void Start()
    {
        m_objectToMove = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Wander();
        AvoidCollisions();
        ApplySteering();
        Reset();
    }
}
