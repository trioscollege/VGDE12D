using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    public Rigidbody2D m_rigidbody2D;

    private float m_horizontal;
    private float m_vertical;
    [HideInInspector]
    public float m_moveSpeed;

    void Start()
    {
        m_moveSpeed = 5.0f;

        if (m_rigidbody2D == null)
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (m_rigidbody2D != null)
        {
            ApplyRigidbodyMovement();
        }
        else
        {
            ApplyNonRigidbodyMovement();
        }
    }

    void ApplyRigidbodyMovement()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");

        m_rigidbody2D.velocity = new Vector2(m_horizontal * m_moveSpeed, m_vertical * m_moveSpeed);
    }

    void ApplyNonRigidbodyMovement()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(m_horizontal, m_vertical, 0.0f) * m_moveSpeed * Time.deltaTime;
    }
}