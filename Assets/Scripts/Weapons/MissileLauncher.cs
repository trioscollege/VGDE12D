using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public float m_speed = 5f;
    public float m_turnSpeed = 100f;
    public float m_rateOfFire = 0.25f;
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
                GameObject missile = PoolManager.Instance.GetObjectForType("Missile", false);
                Missile script = missile.GetComponent<Missile>();
                script.IgnoreCollider = m_equippedTo.GetComponent<Collider2D>();
                script.Target = m_targets[0];
                script.Speed = m_speed;
                script.TurningSpeed = m_turnSpeed;

                missile.transform.position = transform.position;
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