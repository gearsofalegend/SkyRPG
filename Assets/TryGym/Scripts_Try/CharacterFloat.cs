using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFloat : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float land;
    public float boost = 5f;
    public float pop;
    //public float spin;// extra cool points

    private Vector3 rotation;

    private bool isLocked;


    void Start()
    {
        //rotation = new Vector3(0, 0, 0); // extra
        isLocked = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Lock"))
        {
            isLocked = !isLocked;
        }

    }
    void FixedUpdate()
    {
        Floating();

    

    }

    void Floating()
    {
        if (Input.GetButtonDown("Jump"))
        {
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * pop; //initial jump
        }

            if (Input.GetButton("Jump"))
            {
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed; //constant speed

                if (Input.GetButton("Boost"))
                {
                //rotation = new Vector3(0, 1, 0); //extra
                
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * boost; // acceleration 
                //transform.localRotation = transform.localRotation * Quaternion.Euler(rotation * Time.deltaTime * spin); // extra

                 
                }
             

            }
        
        else 
        {

            if (isLocked == false) // falling
            {
                transform.position += new Vector3(0, -1, 0) * Time.deltaTime * land;

                if (Input.GetButton("Boost")) // fall faster
                {
                    //rotation = new Vector3(0, -1, 0); //extra

                    transform.position += new Vector3(0, -1, 0) * Time.deltaTime * boost; // acceleration 
                    //transform.localRotation = transform.localRotation * Quaternion.Euler(rotation * -Time.deltaTime * -spin); // extra

                
                }
                else // just falling
                {
                    //transform.localRotation = Quaternion.identity;
                    //rotation = new Vector3(0, 0, 0);//extra
                   
                }

            }
            else if (isLocked == true) // not falling
            {
                transform.position += new Vector3(0, 0, 0) * Time.deltaTime * land;
            }

        }

    }
}
