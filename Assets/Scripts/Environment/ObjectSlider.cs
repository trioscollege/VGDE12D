using UnityEngine;

public class ObjectSlider : MonoBehaviour
{
    [Tooltip("Normalized at runtime.")]
    public Vector2 m_slideDirection = new Vector2(-1, 0);

    // Start is called before the first frame update
    void Start()
    {
        m_slideDirection = m_slideDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = m_slideDirection * GameManager.Instance.m_gameSpeed * Time.deltaTime;
        transform.position += velocity;
    }
}
