using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    public Collider2D IgnoreCollider { get; set; }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != IgnoreCollider)
        {
            PoolManager.Instance.PoolObject(gameObject);
        }
    }
}
