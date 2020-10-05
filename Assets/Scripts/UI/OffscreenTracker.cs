using UnityEngine;
using UnityEngine.UI;

public class OffscreenTracker : MonoBehaviour
{

    public Image m_trackingArrowImage;
    float m_spriteHalfHeight;
    GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_spriteHalfHeight = m_trackingArrowImage.rectTransform.rect.height * 0.6f;
    }

    // LateUpdate is called once per frame
    void LateUpdate()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(m_player.transform.position);
        if (playerScreenPosition.y > Camera.main.scaledPixelHeight)
        {
            m_trackingArrowImage.enabled = true;
        }
        else
        {
            m_trackingArrowImage.enabled = false;
        }

        if (m_trackingArrowImage.enabled)
        {
            m_trackingArrowImage.rectTransform.position = playerScreenPosition;
            Vector3 arrowPosition = m_trackingArrowImage.transform.position;
            arrowPosition.y = Camera.main.scaledPixelHeight - m_spriteHalfHeight;
            m_trackingArrowImage.rectTransform.position = arrowPosition;
        }
    }
}
