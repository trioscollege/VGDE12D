// using System.Collections;
// using System.Collections.Generic;
// 
// See note in PlayerController.cs re: Commented using statements

using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    // Reference to the PauseScreen gameobject
    public GameObject m_pauseScreen;
    // Reference to the SettingsScreen gameobject
    public GameObject m_settingsScreen;

    // Variables
    public bool m_pauseScreenIsActive = true;
    public bool m_settingsScreenIsActive = false;

    // Functions
    public void SwitchToSettingsScreen(){
        m_pauseScreen.SetActive(false);
        m_settingsScreen.SetActive(true);
        m_pauseScreenIsActive = false;
        m_settingsScreenIsActive = true;
    }

    public void SwitchToPauseScreen(){
        m_pauseScreen.SetActive(true);
        m_settingsScreen.SetActive(false);
        m_pauseScreenIsActive = true;
        m_settingsScreenIsActive = false;
    }
}
