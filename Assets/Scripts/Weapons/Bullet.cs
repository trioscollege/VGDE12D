using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    public Collider2D m_ignoreCollider { get; set; }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != m_ignoreCollider)
        {
            PoolManager.Instance.PoolObject(gameObject);
        }
    }
}
