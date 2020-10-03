using UnityEngine;

public enum ParallaxLayer { ForegroundLayer3 = 1, ForegroundLayer2, ForegroundLayer1, Middleground, Background }

public class ParallaxScroller : MonoBehaviour
{
    public ParallaxLayer m_layer;
    public Transform m_objectOne;
    public Transform m_objectTwo;

    public float m_imageOffset = 32.0f;

    // Update is called once per frame
    void Update()
    {
        m_objectOne.position += Vector3.left * GameManager.Instance.m_gameSpeed / (int)(m_layer) * Time.deltaTime;
        m_objectTwo.position = m_objectOne.position + new Vector3(m_imageOffset, 0, 0);
    
        if (m_objectOne.position.x <= -m_imageOffset)
        {
            m_objectOne.position = m_objectOne.position + new Vector3(m_imageOffset, 0, 0);
            Transform temp = m_objectOne;
            m_objectOne = m_objectTwo;
            m_objectTwo = temp;
        }
    }
}
