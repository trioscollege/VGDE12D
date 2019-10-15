using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerExample : MonoBehaviour
{
    public Text m_hitText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            m_hitText.gameObject.SetActive(true);
            Debug.Log("Player is entering the TriggerCube");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            m_hitText.gameObject.SetActive(false);
            Debug.Log("Player is leaving the TriggerCube");
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.name == "Player"){
            // m_hitText.gameObject.SetActive(true);
            Debug.Log("Player is in the TriggerCube");
            other.GetComponentInParent<SimpleMovement>().m_health++;
        }
    }
}
