using UnityEngine;

public class BasicFleeShip : SteeringBehaviour
{
    public Transform threat;
    public float fleeRadius = 3.0f;

    private void FixedUpdate()
    {
        Flee(threat.position, fleeRadius);
        ApplySteering();
    }
}
