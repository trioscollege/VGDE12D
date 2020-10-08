using UnityEngine;

/*
*   https://kylewbanks.com/blog/create-fullscreen-background-image-in-unity2d-with-spriterenderer
*/

public class FullScreenScript : MonoBehaviour
{
    public int m_orderInLayer;
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector2 scale = transform.localScale;

        if (cameraSize.x >= cameraSize.y)
        {
            scale *= cameraSize.x / spriteSize.x;
        }
        else
        {
            scale *= cameraSize.y / spriteSize.y;
        }

        spriteRenderer.sortingOrder = m_orderInLayer;
        transform.position = Vector3.forward * -m_orderInLayer;
        transform.localScale = scale;
    }
}