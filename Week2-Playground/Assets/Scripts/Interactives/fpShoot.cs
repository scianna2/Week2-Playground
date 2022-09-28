using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpShoot : MonoBehaviour
{

    public enum Shot {nothing, createObject};
    [SerializeField] private Shot shot = Shot.nothing;
    [SerializeField] private GameObject spawnObject;
    private Camera cam;   
    Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2, 0);
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
 
        if(Input.GetMouseButtonDown(0)){
            Ray ray = cam.ScreenPointToRay(screenCenter);
            RaycastHit hit;
            if(Physics.Raycast(ray.origin,ray.direction, out hit)){
                hitTrigger hitTrigger = hit.transform.gameObject.GetComponent<hitTrigger>();
                if (hitTrigger != null){
                    hitTrigger.Activate(ray, hit);
                }
                    
            }
            if(shot == Shot.createObject){
                GameObject spawn;
                if(spawnObject == null){
                    spawn = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                }
                else{
                    spawn = GameObject.Instantiate(spawnObject);
                }
                spawn.transform.position = hit.point;
            }

        }
    }
}
