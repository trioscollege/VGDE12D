using UnityEngine;

public class PlayState : IState
{
    private GameObject m_pausePanel;
    private bool m_isPaused = false;
    private float m_fixedDeltaTime;

    public void OnSceneLoaded() 
    { 
        m_pausePanel = GameObject.FindGameObjectWithTag("PauseMenu");
        m_pausePanel.SetActive(false);
    }

    public void OnStateEnter()
    {
        Cursor.visible = m_isPaused;
        m_fixedDeltaTime = Time.fixedDeltaTime;

        Cursor.visible = false;
    }

    public void OnStateExit()
    {
        // clean up before leaving
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = this.m_fixedDeltaTime * Time.timeScale;
    }

    public void OnStateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_isPaused = !m_isPaused;
            Cursor.visible = m_isPaused;
            Time.timeScale = m_isPaused ? 0.0f : 1.0f;
            Time.fixedDeltaTime = this.m_fixedDeltaTime * Time.timeScale;
            m_pausePanel.SetActive(m_isPaused);
        }
    }
}
