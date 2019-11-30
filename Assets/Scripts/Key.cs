using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

       
    }



    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
           // toolBoxManager.keys++;
            Destroy(this.gameObject);
        }
    }
}
