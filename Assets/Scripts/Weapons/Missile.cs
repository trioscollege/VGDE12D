using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CapsuleCollider2D))]
public class Missile : MonoBehaviour
{
    [HideInInspector]
    public Collider2D IgnoreCollider { get; set; }
    Rigidbody2D m_rigidbody;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public float Speed { get; set; }
    public float TurningSpeed { get; set; }
    public Transform Target { get; set; }

    void FixedUpdate()
    {
        if (Target)
        {
            // Vector3 desiredDirection = Target.position - transform.position;
            // m_rigidbody.velocity = transform.up * Speed;

            // Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, desiredDirection);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, TurningSpeed);

            // determine desired velocity
            Vector3 desiredVelocity = (Target.position - transform.position).normalized * Speed;
            // convert 2D velocity to 3D vector for math purposes
            Vector3 currentVelocity = m_rigidbody.velocity;
            // calculate required steering force vector
            Vector3 steering = desiredVelocity - currentVelocity;
            // cap steering to TurningSpeed
            steering = steering.normalized * TurningSpeed;
            // adjust velocity by adding steering force
            m_rigidbody.AddForce(steering, ForceMode2D.Force);
            // rotate the object toward the direction of travel
            transform.rotation = Quaternion.LookRotation(Vector3.forward, m_rigidbody.velocity);
            
            // visualization lines
            /*Debug.DrawRay(transform.position, desiredVelocity, Color.grey);
            Debug.DrawRay(transform.position, steering, Color.red);
            Debug.DrawRay(transform.position, m_rigidbody.velocity, Color.green);*/
        }
        else
        {
            PoolManager.Instance.PoolObject(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != IgnoreCollider)
        {
            PoolManager.Instance.PoolObject(gameObject);
        }
    }
}