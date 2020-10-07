using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuState : IState
{
     public void OnSceneLoaded() { }

    public void OnStateEnter() { }

    public void OnStateExit() { }

    public void OnStateUpdate() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.NewGameState(new PlayState());
            SceneManager.LoadScene("PlayScene");
        }
    }
}