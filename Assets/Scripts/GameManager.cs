
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    // (Optional) Prevent non-singleton constructor use.
    protected GameManager() { }

    // public bool to track if the game is in a paused state or not.
    [HideInInspector]
    public bool m_isPaused = false;

    public float m_gameSpeed = 1.0f;
    public float m_speedIncrease = 0.1f;

    void Awake() 
    {
         if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        m_gameSpeed += m_speedIncrease * Time.deltaTime;
    }

    /// <summary>
    /// Works by locating the PauseMenu game object via tag and disabling the canvas component.
    /// This is necessary because of the game restart feature, which would invalidate 
    /// the persistent GameManager's reference to any PauseMenu oject in scene.
    /// </summary>
    public void PauseGame()
    {
        // Check to make sure the bool is false, indicating that the game is NOT paused at the moment...
        if (!m_isPaused)
        {
            // Set the timescale of the game to 0. This will disable actions/movements based on Rigidbodies.
            Time.timeScale = 0;
            // Set the m_isPaused bool to true, indicating a paused gamestate.
            m_isPaused = true;
            
            // Turn on the PauseMenu canvas.
            GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = true;
        }
    }

    /// <summary>
    /// Works by locating the PauseMenu game object via tag and enabling the canvas component.
    /// This is necessary because of the game restart feature, which would invalidate 
    /// the persistent GameManager's reference to any PauseMenu oject in scene.
    /// </summary>
    public void UnpauseGame()
    {
        // Check to make sure the bool is true, indicating that the game IS paused at the moment...
        if (m_isPaused)
        {
            // Set the timescale of the game to 1. This will enable actions/movements based on Rigidbodies.
            Time.timeScale = 1;
            // Set the m_isPaused bool to false, indicating an unpaused gamestate.
            m_isPaused = false;

            // Turn off the PauseMenu canvas.
            GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = false;
        }
    }

    public void RestartGame()
    {
        m_isPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("PCGPlatformer");
    }
}
