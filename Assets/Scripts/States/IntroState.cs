using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroState : IState
{
    private float m_countdown = 3.0f;

    public void OnSceneLoaded() { }

    public void OnStateEnter() { }

    public void OnStateExit() { }

    public void OnStateUpdate()
    {
        if (m_countdown > 0)
        {
            m_countdown -= Time.deltaTime;
        }
        else
        {
            GameManager.Instance.NewGameState(new MenuState());
            SceneManager.LoadScene("MenuScene");
        }
    }
}
