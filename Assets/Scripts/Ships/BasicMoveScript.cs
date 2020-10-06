using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class BasicMoveScript : MonoBehaviour
{
    Rigidbody2D m_rigidbody;

    public float m_maxSpeed = 5.0f;
    [Tooltip("The time, in seconds, it take to complete a turn toward a new heading.")]
    public float m_turnTime = 2.0f;
    [Tooltip("m_turnTime +/- m_turnTimeVariance.")]
    public float m_turnTimeVariance = 1.0f;
    private float m_actualTurnTime = 0.0f;

    private Vector3 m_newDirection;
    private float m_startTime;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_rigidbody.gravityScale = 0.0f;

        m_startTime = Time.time;
        m_actualTurnTime = m_turnTime + Random.Range(-m_turnTimeVariance, m_turnTimeVariance);

        // a coroutine to change direction every m_actualTurnTime seconds
        StartCoroutine(NewDirection());
    }

    void FixedUpdate()
    {
        // how much more turning is left in the turn time?
        float fractionComplete = (Time.time - m_startTime) / m_actualTurnTime;
        // adjust velocity toward the new direction over time
        m_rigidbody.velocity = Vector3.Slerp(m_rigidbody.velocity, m_newDirection, fractionComplete);
        // turn the ship toward the velocity direction
        m_rigidbody.transform.rotation = Quaternion.LookRotation(Vector3.forward, m_rigidbody.velocity);
    }

    IEnumerator NewDirection()
    {
        for (; ; )
        {
            m_startTime = Time.time;
            m_newDirection = Random.insideUnitCircle.normalized * m_maxSpeed;
            yield return new WaitForSeconds(m_actualTurnTime);
            m_actualTurnTime = m_turnTime + Random.Range(-m_turnTimeVariance, m_turnTimeVariance);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // collision occured, change course FIXME: Not great for walls
        m_newDirection = (-m_rigidbody.position.normalized + Random.insideUnitCircle.normalized).normalized * m_maxSpeed;
        //m_rigidbody.velocity = (-m_rigidbody.position.normalized + Random.insideUnitCircle.normalized).normalized * m_maxSpeed;
    }
}
