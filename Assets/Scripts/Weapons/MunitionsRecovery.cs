using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunitionsRecovery : MonoBehaviour
{

    [Tooltip("If the munitions escapes the boundaries, this is the distance from the origin at which it's put back into the object pool.")]
    public float m_recoverDistance = 500.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        // fast moving objects may pass right through collisions in simple scenarios, this is to recover lost rockets 
        if (transform.position.magnitude > m_recoverDistance)
        {
            PoolManager.Instance.PoolObject(gameObject);
        }
    }
}
