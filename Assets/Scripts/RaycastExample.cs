using UnityEngine;

public class RaycastExample : MonoBehaviour
{

    RaycastHit2D ObjectHit;
    RaycastHit2D[] ObjectsHit;
    Vector2 m_raycastDirection;

    public int LineLength;
    public string RaycastTargetName;

    void Start(){
        LineLength = 3;
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

        Debug.DrawRay(transform.position, m_raycastDirection*LineLength);
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