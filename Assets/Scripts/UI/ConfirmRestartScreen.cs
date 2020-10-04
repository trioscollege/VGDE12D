using UnityEngine;

public class ConfirmRestartScreen : MonoBehaviour
{
    public GameObject m_pauseScreen;

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void CancelRestart()
    {
        gameObject.SetActive(false);
        m_pauseScreen.SetActive(true);
    }
}
