using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnergyBlast : MonoBehaviour
{

    public bool isMoving = false;
    private GameObject character;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character");
    }

    // Update is called once per frame
    void Update()
    {


        if (isMoving)
        {

            transform.parent = null;
            transform.position += Vector3.forward; //transform.TransformDirection(Vector3.forward);
            //transform.rotation = character.transform;
            Destroy(this.gameObject, 60f);

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
