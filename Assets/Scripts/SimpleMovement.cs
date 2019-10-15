using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    public Rigidbody2D m_rigidbody2D;
    public Rigidbody m_rigidbody3D;

    public float m_horizontal;
    public float m_vertical;
    public float m_moveSpeed;

    public float m_health;
    
    void Start()
    {
        m_moveSpeed = 5.0f;
        m_health = 50.0f;

        if(m_rigidbody2D == null){
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }

        if(m_rigidbody3D == null){
            m_rigidbody3D = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {

        if(m_rigidbody2D != null || m_rigidbody3D != null){
            ApplyRigidbodyMovement();
        }

        if(m_rigidbody2D == null && m_rigidbody3D == null){
            ApplyNonRigidbodyMovement();
        }
    }

    void ApplyRigidbodyMovement(){
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");

        if(m_rigidbody2D != null){
            m_rigidbody2D.velocity = new Vector2(m_horizontal * m_moveSpeed, m_vertical * m_moveSpeed);
        }

        if(m_rigidbody3D != null){
            m_rigidbody3D.velocity = new Vector2(m_horizontal * m_moveSpeed, m_vertical * m_moveSpeed);
        }
    }

    void ApplyNonRigidbodyMovement(){
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(m_horizontal, m_vertical, 0.0f) * m_moveSpeed * Time.deltaTime;
    }
}
