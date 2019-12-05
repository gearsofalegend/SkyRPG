using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlast : MonoBehaviour
{

    public bool isMoving = false;

    // Update is called once per frame
    void Update()
    {


        if (isMoving)
        {

            transform.position += transform.TransformDirection(Vector3.forward);//TODO for fixing shooting direction

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
