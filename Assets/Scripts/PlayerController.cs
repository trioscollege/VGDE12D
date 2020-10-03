using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // Get access to the Rigidbody on this object (The player)    
    Rigidbody2D m_rigidbody;

    // Get access to the Animator on this object
    Animator m_animator;

    // Variables to access input from the player
    float m_horizontalMovement;
    float m_verticalMovement;

    // A boolean to restrict jumping only when on "ground".
    bool m_grounded = false;

    // A static default run speed to be used for sprinting
    static int m_DEFAULT_RUN_SPEED = 5;

    // Speed variable to adjust how fast the player moves
    public float m_moveSpeed = 5.0f;
    // Jump variable to adjust how high the player jumps
    public float m_jumpForce = 7.0f;
    // Layer mask for the ground raycast
    public LayerMask m_groundLayerMask;

    void Start()
    {
        // On start, get the Rigidbody2D component and assign it to m_rigidbody
        m_rigidbody = GetComponent<Rigidbody2D>();

        // On start, get the Animator component and assign it to the m_animator
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Reset the Animation Direction before each scan for input. 
        // This will ensure the character is pointed in the last direction moved and ensure a bool doesn't stay enabled if movement is changed.
        if (!GameManager.Instance.m_isPaused)
        {
            // Look for the Esc keypress and pause the game if Esc is pressed.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.PauseGame();
            }

            // Check the value of the Horizontal Movement. Negative = flip left, Positive = flip right.
            if (m_horizontalMovement < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if (m_horizontalMovement > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
        else
        {
            // If the game is paused and the Esc key is pressed, unpause the game.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.UnpauseGame();
            }
        }

        if (m_horizontalMovement != 0)
        {
            m_animator.SetBool("Running", true);
        }
        else
        {
            m_animator.SetBool("Running", false);
        }

        LocateGround();
    }

    void FixedUpdate()
    {
        // Every frame, check for input from the "Horizontal" and "Vertical" inputs and assign them to the values accordingly
        m_horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (m_grounded && (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump")))
        {
            m_rigidbody.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
        }

        // Add velocity to the Rigidbody component using the horizontal input value, preserving vertical speed (gravity)
        m_rigidbody.velocity = new Vector2(m_horizontalMovement * m_moveSpeed, m_rigidbody.velocity.y);
    }

    void LocateGround()
    {
        // Cast a ray down below the collider
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, m_groundLayerMask);

        // If the raycast hit "ground", tell the animator and permit jumping.
        if (hit)
        {
            m_animator.SetBool("InAir", false);
            m_grounded = true;

            // Attach player to ground to move with the platform
             transform.parent = hit.collider.gameObject.transform;
        }
        else
        {
            m_animator.SetBool("InAir", true);
            m_grounded = false;

            // Detatch the player from the ground to move freely
            transform.parent = null;
        }

        Debug.DrawRay(transform.position, Vector2.down * 0.7f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
       
    }
}