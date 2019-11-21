using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    //the location of the shooting 
    public GameObject missilePrefab;
    public Transform missileSpawn;
   // public int interval;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating	("spawnMissile",1,2);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

   

    void spawnMissile()
    {
        GameObject temp;

        temp = Instantiate(missilePrefab, missileSpawn.position, missileSpawn.rotation);
        Destroy(temp, 15f);
        
        
    }
}
