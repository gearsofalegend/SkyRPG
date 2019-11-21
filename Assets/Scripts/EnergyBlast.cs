using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlast : MonoBehaviour
{

    private float timeBeforeDestroy = 5f;

    public bool isMoving = false;
  
    // Update is called once per frame
    void Update()
    {
        if (Time.time == timeBeforeDestroy)
        {
            
            Destroy	(gameObject);
            
        }

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
