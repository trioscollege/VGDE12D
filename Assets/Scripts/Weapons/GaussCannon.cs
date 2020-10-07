using System.Collections.Generic;
using UnityEngine;

public class GaussCannon : MonoBehaviour
{
    public float m_speed = 15f;
    public float m_rateOfFire = 2f;
    public GameObject m_equippedTo;

    private List<Transform> m_targets;
    private float m_shotDelay = 0f;

    void Awake()
    {
        m_targets = new List<Transform>();
    }

    void Update()
    {

        if (m_shotDelay <= 0)
        {
            if (m_targets.Count > 0)
            {
                // get a bullet from the pool manager and tell it to ignore the ship shooting it
                GameObject bullet = PoolManager.Instance.GetObjectForType("Bullet", false);
                bullet.GetComponent<Bullet>().IgnoreCollider = m_equippedTo.GetComponent<Collider2D>();
                
                // convert the ship's velocity to 3D because we can't perform vector math between 3D and 2D vectors
                Vector3 shipVelocity = m_equippedTo.GetComponent<Rigidbody2D>().velocity;
                // give the bullet a velocity toward the first target in shooting range
                bullet.GetComponent<Rigidbody2D>().velocity = shipVelocity + ((m_targets[0].position - transform.position).normalized * m_speed);
                // start the bullet at the location of the gauss cannon
                bullet.transform.position = transform.position;
                m_shotDelay = 1 / m_rateOfFire;
            }
        }
        else
        {
            m_shotDelay -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != m_equippedTo.GetComponent<Collider2D>())
        {
            m_targets.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other != m_equippedTo.GetComponent<Collider2D>())
        {
            m_targets.Remove(other.transform);
        }
    }
}