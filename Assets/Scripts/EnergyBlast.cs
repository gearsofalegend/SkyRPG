using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlast : MonoBehaviour
{

    private float timeBeforeDestroy = 1f, timeRemaning;

    public bool isMoving = false;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

/*        timeRemaning += Time.deltaTime;
        if (timeRemaning == timeBeforeDestroy)
        {
            print("good");
            Destroy(gameObject);
            
        }*/

        if (isMoving)
        {

            transform.position += transform.TransformDirection(Vector3.forward);


        }

     
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Missile")
        {
            
            Destroy	(other.gameObject);
        }
    }
}
