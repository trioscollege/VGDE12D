using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    public Transform m_target;
    public float m_slowingRadius = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_objectToMove = GetComponent<Rigidbody2D>();       
    }

    void FixedUpdate()
    {
        Seek(m_target.position, m_slowingRadius);
        ApplySteering();
        AvoidCollisions();
        Reset();
    }
}
