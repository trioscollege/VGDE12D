using UnityEngine;

public class BasicChaseShip : SteeringBehaviour
{
    public Transform shipToChase;
    public float slowingRadius = 1.0f;
    private void FixedUpdate()
    {
        Chase(shipToChase.position, slowingRadius);
        ApplySteering();
    }
}
