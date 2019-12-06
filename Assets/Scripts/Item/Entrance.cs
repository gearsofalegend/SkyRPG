using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entrance : MonoBehaviour
{
    public GameObject Player;
     public Material Open, Closed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            //gameObject.GetComponent<MeshRenderer>().materials HOW DO I FETCH A COLOR or MATERIAL? //WHEN YOU EXIT change color
        }
    }

}
