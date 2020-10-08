using System.Collections.Generic;
using UnityEngine;

public class FleeBehaviour : SteeringBehaviour
{
    public float m_fleeSpeedBooster = 1.5f;
    private Collider2D m_hullCollider;
    private float m_baseSpeed;

    private List<Transform> m_threats;
    private Transform m_closestThreat;

    // Start is called before the first frame update
    void Start()
    {
        m_objectToMove = GetComponent<Rigidbody2D>();
        m_hullCollider = GetComponent<Collider2D>();
        m_baseSpeed = m_maxSpeed;

        m_threats = new List<Transform>();
    }

    void FixedUpdate()
    {
        FindClosestThreat();

        if (m_closestThreat)
        {
            // give it all we got, captain!
            m_maxSpeed = m_baseSpeed * m_fleeSpeedBooster;
            Flee(m_closestThreat.position);
        }
        else
        {
            // impulse power!
            m_maxSpeed = m_baseSpeed;
            Wander();
        }
        AvoidCollisions();
        ApplySteering();
        Reset();
    }

    private void FindClosestThreat()
    {
        // check if the threat was deactivated (projectiles, for example)
        if (m_closestThreat && !m_closestThreat.gameObject.activeSelf)
        {
            m_threats.Remove(m_closestThreat);
            m_closestThreat = null;
        }

        float nearestThreatDistance = float.MaxValue;
        foreach (Transform threat in m_threats)
        {
            float distanceToThreat = (threat.position - transform.position).magnitude;
            if (distanceToThreat < nearestThreatDistance)
            {
                m_closestThreat = threat;
                nearestThreatDistance = distanceToThreat;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // We don't want to flee from targeters, the boundary, or ourself, and this is easier than fiddling with layers
        if (collider.gameObject.layer != LayerMask.NameToLayer("Boundary") &&
            collider.gameObject.layer != LayerMask.NameToLayer("Targeter"))
        {
            // add the threat to the list
            m_threats.Add(collider.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // We don't want to flee from targeters, the boundary, or ourself, and this is easier than fiddling with layers
        if (collider.gameObject.layer != LayerMask.NameToLayer("Boundary") &&
            collider.gameObject.layer != LayerMask.NameToLayer("Targeter"))
        {
            // stop tracking the threat, it's at a safe distance
            m_threats.Remove(collider.transform);

            if (m_threats.Count == 0)
            {
                m_closestThreat = null;
            }
        }
    }
}
