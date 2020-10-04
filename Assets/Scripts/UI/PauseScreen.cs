using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{    public GameObject m_confirmScreen;

    public void ConfirmRestart() {
        gameObject.SetActive(false);
        m_confirmScreen.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit(0);
    }
}
