using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMechanics : MonoBehaviour
{

    public float upForce;

    private float moveX;
    private float moveZ;

    public Vector3 _inputs;
    public float Speed = 5;


    enum GravityState
    {
        Ground,
        Flying
    }


    private GravityState characterState;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddRelativeForce(Vector3.forward*15,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    
         _inputs = Vector3.zero;
         _inputs.x = Input.GetAxis("Horizontal");
         _inputs.z = Input.GetAxis("Vertical");

        characterStateMethod();


    }



    void characterStateMethod()
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {


            if (hit.distance > 1.08f)
            {
                //canJump = false;
                characterState = GravityState.Flying;

            }


            //will check the hit distance and verify if we can jump or not 
            if (hit.distance < 1.08f)
            {
                //canJump = true;
                characterState = GravityState.Ground;
                // print("StateG " + characterState);
            }

         

          //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }
        else
        {
          //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        }

    }


    void FloatMechanics()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up  * upForce , ForceMode.Acceleration	);
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.useGravity = false;
            rb.mass = 1;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.useGravity = true;
            rb.mass = 5;
        }
        

        if (Input.GetKeyDown(KeyCode.J))
        {

            rb.isKinematic = true;
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            rb.AddRelativeForce(Vector3.forward *2, ForceMode.VelocityChange);
        } 
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            rb.velocity = Vector3.zero;
        }
    }





    private void FixedUpdate()
    {
       FloatMechanics();
       //rb.MovePosition(rb.position + _inputs * Speed * Time.fixedDeltaTime);
       //rb.position += _inputs * Speed * Time.fixedDeltaTime;
 
       transform.Translate(_inputs * Time.fixedDeltaTime *Speed);
       //rb.rotation *= Quaternion.LookRotation(_inputs);
       //rb.rotation *= Quaternion.LookRotation(_inputs * Time.fixedDeltaTime);

    }
    
    


    
    
    
}
