using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // (Optional) Prevent non-singleton constructor use.
    protected GameManager() { }

    // public reference of the PauseMenu object to allow access to turn the gameobject on and off when the game is paused.
    public GameObject m_pauseMenu;
    
    // public bool to track if the game is in a paused state or not.
    [HideInInspector]
    public bool m_isPaused = false;

    public float m_gameSpeed = 1.0f;
    public float m_speedIncrease = 0.1f;

    void Update() 
    {
        m_gameSpeed += m_speedIncrease * Time.deltaTime;
    }

    // Function to pause the game and activate the PauseMenu object.
    public void PauseGame()
    {
        // Check to make sure the bool is false, indicating that the game is NOT paused at the moment...
        if (!m_isPaused)
        {
            // Set the timescale of the game to 0. This will disable actions/movements based on Rigidbodies.
            Time.timeScale = 0;
            // Set the m_isPaused bool to true, indicating a paused gamestate.
            m_isPaused = true;
            // Turn on the PauseMenu gameobject.
            m_pauseMenu.SetActive(true);
        }
    }

    // Function to unpause the game and deactivate the PauseMenu object.
    public void UnpauseGame()
    {
        // Check to make sure the bool is true, indicating that the game IS paused at the moment...
        if (m_isPaused)
        {
            // Set the timescale of the game to 1. This will enable actions/movements based on Rigidbodies.
            Time.timeScale = 1;
            // Set the m_isPaused bool to false, indicating an unpaused gamestate.
            m_isPaused = false;
            // Turn off the PauseMenu gameobject.
            m_pauseMenu.SetActive(false);
        }
    }
}
