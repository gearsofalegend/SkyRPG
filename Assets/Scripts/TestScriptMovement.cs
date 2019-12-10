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

    public float dashSpeed;

    enum GravityState
    {
        Ground,
        Flying
    }

    private GravityState characterState;

    private float horizontalStrafeAxis,
        verticalStrafeAxis,
        zMovementAxis,
        rotationAxis;


    public int speed;
    public Vector3 direction;


    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animation and colliders
        animator = GetComponent<Animator>();
        swordCollider = GameObject.FindWithTag("SkySword").GetComponent<MeshCollider>();

        lastPosition = transform.position;//a test ...
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
   
        Dash();

        animator.SetFloat("Jog", verticalStrafeAxis);

        Animations();
        characterStateMethod();
    }

    bool rotationAxisPositive;
    bool rotationAxisNegative;
    private bool zMovementAxisPositive;
    private bool zMovementNegative;
    
    void CharacterMove()
    {
        horizontalStrafeAxis = Input.GetAxis("Horizontal"); //this variable collects the value of the button pressed (1,0, or -1)
        verticalStrafeAxis = Input.GetAxis("Vertical");

        rotationAxisPositive = Input.GetKey(KeyCode.Keypad6);
        rotationAxisNegative = Input.GetKey(KeyCode.Keypad4);

        zMovementAxisPositive = Input.GetKey(KeyCode.Q);
        zMovementNegative = Input.GetKey(KeyCode.E);

        rotationAxis = rotationAxisPositive? (rotationAxisNegative? (0) : (1) ) : (rotationAxisNegative? (-1) : (0) );
        
        zMovementAxis = zMovementAxisPositive? (zMovementNegative? (0) : (1) ) : (zMovementNegative? (-1) : (0) );

        direction = new Vector3(horizontalStrafeAxis,verticalStrafeAxis, zMovementAxis) ;

        //Vector3 difference = ;
        //Quaternion temp = Quaternion.LookRotation((transform.position - lastPosition),Vector3.right);

        /*if (horizontalAxis != 0 || verticalAxis != 0)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
 
        //transform.Translate(direction,Space.World); //moves character 

        transform.position += direction;*/


        //lastPosition = transform.position;//update last position

        
        //rb.position+= transform.TransformDirection(direction * Time.deltaTime * speed);
        //Enable this line once the fix is done for the physic / animator component
        //rb.MovePosition(transform.position + transform.TransformDirection(direction * Time.deltaTime * speed));

        transform.position += transform.TransformDirection(direction * Time.deltaTime * speed);
        transform.Rotate(Vector3.up, rotationAxis, Space.Self);

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
