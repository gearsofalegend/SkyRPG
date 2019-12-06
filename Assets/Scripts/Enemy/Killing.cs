using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killing : MonoBehaviour
{
    //public GameObject Player;
    public Transform Checkpoint;

    // Start is called before the first frame update
    void Start()
    {
        if (Checkpoint == null)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = Checkpoint.transform.position;
            //Destroy(other.gameObject);
        }
    }
}
