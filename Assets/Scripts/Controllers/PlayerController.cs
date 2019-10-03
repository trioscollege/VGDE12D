// using System.Collections;
// using System.Collections.Generic;
// 
// These using directives are commented out, rather than removed all together...
// As a personal habit, I tend to include these on scripts where I may need them in the future.
// In VSCode, Ctrl + / (PC) or Cmd + / (Mac) will either add or remove a comment block on a line, 
// or combination of lines you have selected/highlighted.
// It's easier to simply add or remove the comment rather than retype the using line all together.

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Get access to the Rigidbody on this object (The player)
    Rigidbody2D m_rigidbody;

    // Variables to access input from the player
    float m_horizontalMovement;
    float m_verticalMovement;

    // Speed variable to adjust how fast the player moves on screen
    public float m_moveSpeed;
    
    // A static default run speed to be used for sprinting
    static int m_DEFAULT_RUN_SPEED = 5;

    void Start ()
    {
        // On start, get the Rigidbody2D component and assign it to m_rigidbody
        m_rigidbody = GetComponent<Rigidbody2D>(); 
    }

    void Update(){
        // Every frame, check for input from the Left Shift key and act accordingly
        if(Input.GetKey(KeyCode.LeftShift)){
            m_moveSpeed = m_DEFAULT_RUN_SPEED * 2;
        } else {
            m_moveSpeed = m_DEFAULT_RUN_SPEED;
        }
    }

    void FixedUpdate ()
    {
        // Every frame, check for input from the "Horizontal" and "Vertical" inputs and assign them to the values accordingly
        m_horizontalMovement = Input.GetAxisRaw("Horizontal");
        m_verticalMovement = Input.GetAxisRaw("Vertical"); 

        // Add velocity to the Rigidbody component using the inputs values, adding the relative move speed we've assigned earlier
        m_rigidbody.velocity = new Vector2(m_horizontalMovement * m_moveSpeed, m_verticalMovement * m_moveSpeed);
    }
}