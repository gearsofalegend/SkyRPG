using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallShooting : MonoBehaviour
{
    public KeyCode fireKey;
    public GameObject energyBlast;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
//        float moveX = Input.GetAxis("Horizontal");
//        float moveY = Input.GetAxis("Vertical");

      //  MoveCube(moveX, moveY, Time.deltaTime * 35.0f);

        FireEnergyMethod();
    }


//    void MoveCube(float x, float z, float speed = 5.0f)
//    {
//        //
//        transform.Translate(new Vector3(x * speed, 0, z * speed));
//    }


    GameObject temp;

    void FireEnergyMethod()//TODO cooldown
    {
        if (Input.GetKeyDown(fireKey)) 
        {
            temp = Instantiate(energyBlast, transform.position, transform.rotation);
            temp.GetComponent<SphereCollider>().enabled = false;
        }

        if (Input.GetKey(fireKey) && temp)
        {
            temp.transform.position = transform.position;
            temp.transform.rotation = transform.rotation;
        }

        if (Input.GetKeyUp(fireKey) && temp)
        {
            temp.GetComponent<SphereCollider>().enabled = true;
            temp.GetComponent<EnergyBlast>().isMoving = true;

            Destroy(temp, 2f);
        }
    }
    

}