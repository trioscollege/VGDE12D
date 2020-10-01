using UnityEngine;

public class RaycastExample : MonoBehaviour
{

    RaycastHit2D ObjectHit;
    RaycastHit2D[] ObjectsHit;
    Vector2 m_raycastDirection;

    public int LineLength;
    public string RaycastTargetName;
    public bool m_drawRays = false;
    private LineRenderer m_lineRenderer;

    void Start(){
        LineLength = 3;
        
        // This is used to draw our raycast in both the Scene and Game windows.
        m_lineRenderer = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        m_lineRenderer.sortingOrder = 10;
        m_lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        m_lineRenderer.startColor = Color.cyan;
        m_lineRenderer.endColor = Color.cyan;
        m_lineRenderer.widthMultiplier = 0.05f;
        m_lineRenderer.positionCount = 2;
    }
    
    void Update(){

        SetRaycastDirection();

        PerformRaycast();

        // PerformRaycastAll();
    }

    void SetRaycastDirection(){
        if(Input.GetKey(KeyCode.A)){
            m_raycastDirection = Vector2.left;
        }

        if(Input.GetKey(KeyCode.D)){
            m_raycastDirection = Vector2.right;
        }
        
        if(Input.GetKey(KeyCode.S)){
            m_raycastDirection = Vector2.down;
        }
        
        if(Input.GetKey(KeyCode.W)){
            m_raycastDirection = Vector2.up;
        }

        // If the user enabled the m_drawRays flag in the inspect, the line renderer will be updated to draw the active raycast.
        if (m_drawRays)
        {
            m_lineRenderer.SetPosition(0, transform.position);
            m_lineRenderer.SetPosition(1, transform.position + new Vector3(m_raycastDirection.x, m_raycastDirection.y, 0) * LineLength);
        }
    }

    void PerformRaycast(){
        ObjectHit = Physics2D.Raycast(transform.position, m_raycastDirection, LineLength, LayerMask.GetMask("Colliders"));

        if(ObjectHit.collider != null){
            RaycastTargetName = ObjectHit.collider.name;
            Debug.Log(ObjectHit.collider.name);
        } else {
            RaycastTargetName = "";
            Debug.Log("No Hit Detected");
        }
    }

    void PerformRaycastAll(){
        ObjectsHit = Physics2D.RaycastAll(transform.position, m_raycastDirection, LineLength, LayerMask.GetMask("Colliders"));

        for(int i = 0; i < ObjectsHit.Length; i++){
            Debug.Log(ObjectsHit[i].collider.name);
        }
    }
}