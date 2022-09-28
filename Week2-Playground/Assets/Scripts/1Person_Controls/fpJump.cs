using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpJump : MonoBehaviour
{
    [SerializeField] private float gravity = .00981f;
    [SerializeField] private float terminalVelocity = -100f;
    [SerializeField] private bool canJump = false;
    [SerializeField] private float jumpVelocity = 10f;
    [HideInInspector] public float velocityY = 0;
    private float yOffset;
    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        //This makes sure there is a Character Controller component so that the script can run
        cc = gameObject.GetComponent<CharacterController>();
        if (cc==null){Debug.Log("Missing CharacterController");}
        CapsuleCollider cap = gameObject.GetComponent<CapsuleCollider>();
        if (cap==null){Debug.Log("Missing Capsule Collider");}

        yOffset = (cap.height/2);
    }

    // Update is called once per frame
    void Update()
    {

        //Checks to see if we're on the ground
        bool isGrounded = false;
        Vector3 bottomPoint = transform.position;
        float rOffset = cc.radius*0.75f;
        float rOffsetY = cc.radius*0.5f;
        Vector3[] offsetRay = new [] {new Vector3(0f,0f,0f), new Vector3(rOffset,rOffsetY,0), new Vector3(-rOffset, rOffsetY, 0), new Vector3(0,rOffsetY,rOffset), new Vector3(0,rOffsetY,-rOffset)};
        bottomPoint.y = bottomPoint.y - yOffset;
        foreach(Vector3 oray in offsetRay){
            if(Physics.Raycast(bottomPoint+oray, Vector3.down, 0.3f)){
             isGrounded = true;
             }
             Debug.DrawLine(bottomPoint+oray, bottomPoint+oray + (Vector3.down*0.3f), Color.green);   
        }
       


        if (isGrounded){
            //If the character is on the ground, do not increase velocity
            if(canJump && Input.GetButtonDown("Jump")){
                Debug.Log("Attempted to jump");
                velocityY = jumpVelocity;
            }
        }
        else{
            velocityY -= gravity;
            velocityY = Mathf.Max(velocityY, terminalVelocity);
        }

        cc.Move((transform.up * velocityY) * Time.deltaTime);
        
    }


}
