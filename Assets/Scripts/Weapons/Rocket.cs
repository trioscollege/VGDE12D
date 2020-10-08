using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CapsuleCollider2D))]
public class Rocket : MonoBehaviour
{
    [HideInInspector]
    public Collider2D IgnoreCollider { get; set; }
    Rigidbody2D m_rigidbody;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public float Acceleration { get; set; }

    // Update is called once per frame
    void FixedUpdate()
    {
        //apply forward acceleration
        m_rigidbody.AddForce(m_rigidbody.velocity * Acceleration, ForceMode2D.Force);
        //face the sprite in thedirection we are moving.
        transform.rotation = Quaternion.LookRotation(Vector3.forward, m_rigidbody.velocity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != IgnoreCollider)
        {
            PoolManager.Instance.PoolObject(gameObject);
        }
    }
}