﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private Animator animator;
    private MeshCollider swordCollider; //for swordStrike collider (damage)

    public float dashSpeed;

    enum GravityState
    {
        Ground,
        Flying
    }

    private GravityState characterState;

    private float horizontalAxis,
        verticalAxis;


    public int speed;
    public Vector3 direction;
    //public float jumpforce;
    //public float dashforce;
    //private float gravityforce = 9.807f; // optional can do later

    private Rigidbody rb;
    //private bool isgrounded;

    //public float decceleration;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animation and colliders
        animator = GetComponent<Animator>();
        swordCollider = GameObject.FindWithTag("SkySword").GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CharMove();
        //Jump();
        //GetDirectionInput();
        Dash();

        animator.SetFloat("Jog", verticalAxis);

        Animations();
        characterStateMethod();
    }

    void CharMove()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal"); //this variable collects the value of the button pressed (1,0, or -1)
        verticalAxis = Input.GetAxisRaw("Vertical");


        Vector3 verticalMovement = transform.forward * verticalAxis;
        //Vector3 moveSide = transform.right * horizontalAxis;

        gameObject.transform.position += verticalMovement * Time.deltaTime * speed;

        
        Vector3 moveSide = transform.right * horizontalAxis;

        direction = (verticalMovement + moveSide);


    }

    //    void Jump()
    //    {
    //        if (isgrounded) // can remoce "True" because just saying it means its true.. "!" before will make it opposite (false).. "!=" means opposite of current result (isDead example with Time)
    //        {
    //            if (Input.GetButtonDown("Jump"))
    //            {
    //
    //                rb.AddForce(0, jumpforce, 0, ForceMode.Impulse); //I can jump one and dash forever, I might keep that
    //
    //
    //            }
    //
    //        }
    //    }

    //    void Dash() // This feature punishes players that use it too much. It gets harder to control
    //    {
    //        if (isgrounded)
    //        {
    //            if (Input.GetButtonDown("Dash"))
    //            {
    //                rb.AddForce(direction * dashforce, ForceMode.Impulse); // because in this case direction is a Vector3
    //
    //            }
    //
    //            Debug.Log(rb.velocity);
    //
    //            rb.velocity -= rb.velocity * Time.deltaTime * decceleration;
    //
    //            
    //        }
    //    }

    //    void GetDirectionInput()
    //    {
    //        float horizontalAxis =
    //            Input.GetAxisRaw("Horizontal"); //this variable collects the value of the button pressed (1,0, or -1)
    //
    //        float verticalAxis = Input.GetAxisRaw("Vertical");
    //
    //
    //
    //        Vector3 moveForward = transform.forward * verticalAxis;
    //        Vector3 moveSide = transform.right * horizontalAxis;
    //
    //
    //
    //        direction = (moveForward + moveSide);
    //
    //    }

    //    private void OnCollisionEnter(Collision collision)
    //    {
    //        if (collision.gameObject.tag == "Ground")
    //        {
    //            isgrounded = true;
    //           
    //        }
    //
    //
    //    }


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
            //rb.AddRelativeForce(-direction * -dashSpeed, ForceMode.Impulse);
            rb.AddRelativeForce(transform.forward * dashSpeed, ForceMode.Impulse); //Original
        }
        else
        {
            return;
            //Keep center of gravity
        }

    }

}