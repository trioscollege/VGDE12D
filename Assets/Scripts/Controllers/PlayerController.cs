﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Get access to the Rigidbody on this object (The player)
    Rigidbody2D m_rigidbody;

    // Get access to the Animator on this object
    public Animator m_animator;

    // Variables to access input from the player
    float m_horizontalMovement;
    float m_verticalMovement;

    // A simple check to see if our player is giving any movement input
    public bool m_isMoving = false;

    // Speed variable to adjust how fast the player moves on screen
    public float m_moveSpeed;
    
    // A static default run speed to be used for sprinting
    static int m_DEFAULT_RUN_SPEED = 5;

    void Start ()
    {
        // On start, get the Rigidbody2D component and assign it to m_rigidbody
        m_rigidbody = GetComponent<Rigidbody2D>(); 

        // On start, get the Animator component and assign it to the m_animator
        m_animator = GetComponent<Animator>();
    }

    void Update(){
        // Reset the Animation Direction before each scan for input. 
        // This will ensure the character is pointed in the last direction moved and ensure a bool doesn't stay enabled if movement is changed.
        ResetAnimDirecetion();

        // Check for input from AWSD keys.
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)){
            // If movement is detected, mark m_isMoving as true.
            m_isMoving = true;
            // Call to the Animator and set the Walk layer weight to 1.
            m_animator.SetLayerWeight(1,1);

            // If the input given is Left or Right...
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
                // Check the value of the Horizontal Movement. Negative = Left, Positive = Right.
                if(m_horizontalMovement < 0){
                    // Left movement detected, call to Animator and set the Left bool to true.
                    m_animator.SetBool("Left", true);
                } else {
                    // Right movement detected, call to Animator and set the Right bool to true.
                    m_animator.SetBool("Right", true);
                }
            // If the input given is Up or Down (Forward or Backward)...    
            } else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)){
                if(m_verticalMovement < 0){
                    // Forward movement detected, call to Animator and set the Forward bool to true.
                    m_animator.SetBool("Forward", true);
                } else {
                    // Backward movement detected, call to Animator and set the Backward bool to true.
                    m_animator.SetBool("Backward", true);
                }
            } 
        // No movement input detected
        } else {
            // Set m_isMoving to false...
            m_isMoving = false;
            // Call to the Animator and set the Walk layer weight back to 0.
            m_animator.SetLayerWeight(1,0);
            // Call to the Animator and set the Run layer weight back to 0.
            m_animator.SetLayerWeight(2,0);
        }
        
        // If the m_isMoving bool is true, check for the Left Shift key input.
        if(m_isMoving){
            // Left Shift input detected
            if(Input.GetKey(KeyCode.LeftShift)){
                // Call to the Animator and set the Run layer weight to 1.
                m_animator.SetLayerWeight(2,1);
                // Double the m_moveSpeed to make the character "Run".
                m_moveSpeed = m_DEFAULT_RUN_SPEED * 2;
            // Left Shift input NOT detected
            } else {
                // Call to the Animator and set the Run layer weight back to 0.
                m_animator.SetLayerWeight(2,0);
                // Reset the m_moveSpeed so the character walks.
                m_moveSpeed = m_DEFAULT_RUN_SPEED;
            }
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

    // Quick function to reset all bool parameters in the Animator.
    void ResetAnimDirecetion(){
        m_animator.SetBool("Left", false);
        m_animator.SetBool("Right", false);
        m_animator.SetBool("Forward", false);
        m_animator.SetBool("Backward", false);
    }
}