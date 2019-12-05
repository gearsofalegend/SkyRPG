using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameManager.GetInstance();
       
    }



    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            gameManager.keys++;
            Destroy(this.gameObject);
        }
    }
}
