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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target)
        {
            Vector3 desiredDirection = Target.position - transform.position;
            m_rigidbody.velocity = transform.up * Speed;

            Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, desiredDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, TurningSpeed * Time.deltaTime);
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