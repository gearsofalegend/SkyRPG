using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestScriptMovement : MonoBehaviour
{

    private Animator animator;
    private MeshCollider swordCollider; //for swordStrike collider (damage)

    private Vector3 lastPosition; //will be using this for rotation...

    private float rightStickHorizontal, rightStickVertical;
    private float leftStickHorizontal, leftStickVertical;

    public bool isStrafing;

    public GameObject camera;
    
    

    enum GravityState
    {
        Ground,
        Flying
    }

    private GravityState characterState;

    
    public int speed;
    public Vector3 directionJoyStick;


    private Rigidbody rb;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animation and colliders
        animator = GetComponent<Animator>();
        swordCollider = GameObject.FindWithTag("SkySword").GetComponent<MeshCollider>();

        offset = camera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
   
        Dash();

        animator.SetFloat("Jog", rightStickVertical);

        Animations();
        characterStateMethod();
    }

    bool rotationAxisPositive;
    bool rotationAxisNegative;
    private bool zMovementAxisPositive;
    private bool zMovementNegative;
    
    void CharacterMove()
    {
        rightStickHorizontal = Input.GetAxis("RightStickXAxis");
        rightStickVertical = Input.GetAxis("RightStickYAxis");
 
        leftStickHorizontal = Input.GetAxis("Horizontal"); //this variable collects the value of the button pressed (1,0, or -1)
        leftStickVertical = Input.GetAxis("Vertical");


        if (isStrafing)
        {
            directionJoyStick = new Vector3(leftStickHorizontal,leftStickVertical,rightStickVertical);
            transform.position += transform.TransformDirection( directionJoyStick * Time.deltaTime * speed);// takes account character "LOCAL POSITION" 
            transform.Rotate(Vector3.up, rightStickHorizontal, Space.Self);//TODO important method 

            
        }
        else
        { 
           
            
            directionJoyStick = new Vector3(camera.transform.TransformDirection(camera.transform.right).x * leftStickHorizontal,0,camera.transform.TransformDirection(camera.transform.forward).z * leftStickVertical);
            
            if ( leftStickHorizontal != 0 || leftStickVertical != 0)//to avoid returning to zero in rotation
            {
                transform.rotation = Quaternion.LookRotation(directionJoyStick); //can be used with slerp for smoothing result
            }

            
            
          // transform.position+= directionJoyStick.normalized * Time.deltaTime * speed;
            transform.position += transform.TransformDirection(directionJoyStick.normalized * Time.deltaTime * speed);


        }



/*

        
       // directionJoyStick = new Vector3(rightStickHorizontal,0,rightStickVertical);
        

        /*if (horizontalAxis != 0 || verticalAxis != 0)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
 
        //transform.Translate(direction,Space.World); //moves character 

        transform.position += direction;*/


        
        //rb.position+= transform.TransformDirection(direction * Time.deltaTime * speed);
        //Enable this line once the fix is done for the physic / animator component
        //rb.MovePosition(transform.position + transform.TransformDirection(direction * Time.deltaTime * speed));
        
  // transform.Rotate(Vector3.up, rotationAxis, Space.Self);
    }

    private void LateUpdate()
    {
        if (!isStrafing)
        {
            offset = Quaternion.AngleAxis (rightStickHorizontal * 2, Vector3.up) * offset;
            camera.transform.position = transform.position + offset;
            camera.transform.LookAt(transform.position);
            //camera.transform.RotateAround(transform.position-offset,Vector3.up, rightStickHorizontal);

        }
    }


    void characterStateMethod()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.distance > 0.5f)
            {
                //canJump = false;
                characterState = GravityState.Flying;
                print("State " + characterState);

            }


            //will check the hit distance and verify if we can jump or not 
            if (hit.distance < 0.5f)
            {
                //canJump = true;
                characterState = GravityState.Ground;
                print("State " + characterState);
            }

         
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
             
        }
        else
        {

            characterState = GravityState.Flying;
      
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.white);
        }
       // print("distance "+ hit.distance);
    }

    void Animations()
    {
        ///ANIMATIONS
        if (Input.GetButtonDown("Sword"))
        {
            animator.SetTrigger("swordStrike");
            swordCollider.enabled = true;
            
        }else if (animator.GetCurrentAnimatorStateInfo(0).IsName("SwordStrike") )
        {
            swordCollider.enabled = false;
        }


        switch (characterState)
        {
            case GravityState.Flying:
                animator.SetBool("flyForward", true);
                break;
            default:
                animator.SetBool("flyForward", false);
                break;
            
        }
        
//
//        if (Input.GetKeyDown(KeyCode.U))
//        {
//            animator.Play("special");
//        }
//
////        if (Input.GetKeyUp(KeyCode.U))
////        {
////
////            animator.enabled = false;
////        }
        

    }



    void Dash()
    {
        if (Input.GetButton("Dash"))
        {
            //rb.AddForce(-direction * -dashSpeed, ForceMode.Impulse);
            //rb.AddRelativeForce(transform.forward * dashSpeed, ForceMode.Impulse); //Original
        }
        else
        {
            return;
            //Keep center of gravity
        }

    }


}
