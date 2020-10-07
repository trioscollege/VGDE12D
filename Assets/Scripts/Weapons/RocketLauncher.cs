using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public float m_acceleration = 7f;
    public float m_rateOfFire = 1f;
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
                GameObject rocket = PoolManager.Instance.GetObjectForType("Rocket", false);
                Rocket script = rocket.GetComponent<Rocket>();
                script.IgnoreCollider = m_equippedTo.GetComponent<Collider2D>();
                
                // start the rocket in the right direction
                rocket.GetComponent<Rigidbody2D>().AddForce((m_targets[0].position - transform.position).normalized * m_acceleration, ForceMode2D.Force);
                // give the rocket an acceleration
                script.Acceleration = m_acceleration;
                // start the rocket at the location of the launcher
                rocket.transform.position = transform.position;
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