using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider trig){
        collideTrigger ct = trig.transform.gameObject.GetComponent<collideTrigger>();
                if (ct != null){
                    ct.Activate(this.gameObject);
                }

    }
}
