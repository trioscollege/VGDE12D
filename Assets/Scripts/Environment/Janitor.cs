using UnityEngine;

public class Janitor : MonoBehaviour
{
    [Tooltip("The distance, in units, from the PlatformGenerator when the object is cleaned up.")]
    public float m_garbageDistance = 25.0f;

    // Update is called once per frame
    void Update()
    {
        float distance = transform.localPosition.magnitude;

        if (distance >= m_garbageDistance)
        {
            Destroy(gameObject);
        }
    }
}
