using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragController : MonoBehaviour
{
    private int layerMask;
    Camera camera;
    RaycastHit hit;
    public GameObject hitObject;

    private bool isDragging = false;

    private long distance = 500;

    void Start()
    {
        camera = Camera.main;    
        Debug.Log(camera);
    }

    void FixedUpdate(){

        Ray ray = camera.ScreenPointToRay(Input.mousePosition); 
        
        if(isDragging){
            if(Physics.Raycast(ray, out hit, distance, 1 << 8)){                    
                Debug.Log(hit.point);
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
                UpdateActiveObjectPosition(hit.point);
            }
        } else {
            if(Physics.Raycast(ray, out hit, distance, 1)){
                if(hit.transform.gameObject.tag == "Tower"){
                    hitObject = hit.transform.gameObject;
                    Debug.Log("Hit Tower" + hit.transform.name);
                } else {
                    if(hitObject!=null){                        
                        hitObject = null;
                    }
                }
            } else {
                if(hitObject!=null){                        
                    hitObject = null;
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
        }
    }

    void Update(){
        HandleMouseEvents();
    }

    void HandleMouseEvents(){
        if(Input.GetMouseButtonDown(0)){
            if(hitObject != null){                                        
                isDragging = true;
            }
        }
        if(Input.GetMouseButtonUp(0)){                
            if(hitObject){
                isDragging = false;
            }
        }
    }

    void UpdateActiveObjectPosition(Vector3 newPosition){    
        if(hitObject!=null){                
            hitObject.transform.position = newPosition;
        }
    }


}
