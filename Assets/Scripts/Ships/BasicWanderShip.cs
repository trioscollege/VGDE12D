using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWanderShip : SteeringBehaviour
{

    private void FixedUpdate()
    {
        Wander();
        ApplySteering();
    }
}
