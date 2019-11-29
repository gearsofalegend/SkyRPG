﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int speed;
    public Vector3 direction;
    public float jumpforce;
    public float dashforce;
    private float gravityforce = 9.807f; // optional can do later
    private Rigidbody rb;
    private bool isgrounded;
   /// private bool secondjumpavailable;
    public float decceleration;



    // Start is called before the first frame update
    void Start()
    {
        //secondjumpavailable = true;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CharMove();
        Jump();
        GetDirectionInput();
        Dash();
    }

    void CharMove()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal"); //this variable collects the value of the button pressed (1,0, or -1)

        float verticalAxis = Input.GetAxisRaw("Vertical");

        //this.gameObject.transform.position += new Vector3(horizontalAxis, 0, verticalAxis) * Time.deltaTime * speed;

        Vector3 moveForward = transform.forward * verticalAxis;
        Vector3 moveSide = transform.right * horizontalAxis;

        gameObject.transform.position += (moveForward + moveSide) * Time.deltaTime * speed;


    }

    void Jump()
    {
        if (isgrounded) // can remoce "True" because just saying it means its true.. "!" before will make it opposite (false).. "!=" means opposite of current result (isDead example with Time)
        {
            if (Input.GetButtonDown("Jump"))
            {

                rb.AddForce(0, jumpforce, 0, ForceMode.Impulse); //I can jump one and dash forever, I might keep that


              /* if (!secondjumpavailable)
                {
                    isgrounded = false;
                }
                else
                {
                    secondjumpavailable = false;
                }*/
            }

        }
    }

    void Dash() // This feature punishes players that use it too much. It gets harder to control
    {
        if (isgrounded)
        {
            if (Input.GetButtonDown("Dash"))
            {
                rb.AddForce(direction * dashforce, ForceMode.Impulse); // because in this case direction is a Vector3

            }

            Debug.Log(rb.velocity);

            rb.velocity -= rb.velocity * Time.deltaTime * decceleration;

            //have to make a "isRunning" for when I throw the ball I am still able to throw forward
        }
    }

    void GetDirectionInput()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal"); //this variable collects the value of the button pressed (1,0, or -1)

        float verticalAxis = Input.GetAxisRaw("Vertical");

        //this.gameObject.transform.position += new Vector3(horizontalAxis, 0, verticalAxis) * Time.deltaTime * speed;

        Vector3 moveForward = transform.forward * verticalAxis;
        Vector3 moveSide = transform.right * horizontalAxis;

        //direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        direction = (moveForward + moveSide);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isgrounded = true;
            //secondjumpavailable = true;
        }


    }

}