using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject explosion;
    
    //the location of the shooting 
    public GameObject missilePrefab;
    public Transform missileSpawn;

    public float cannonHealth = 3;

    public bool isShooting;
    
    // Start is called before the first frame update
    void Start()
    {
       // InvokeRepeating	("spawnMissile",1,2);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting) 
        {
            InvokeRepeating	("spawnMissile",1,2);
            isShooting = false;

        }
    }

   

    void spawnMissile()
    {
        GameObject temp;

        temp = Instantiate(missilePrefab, missileSpawn.position, missileSpawn.rotation);
        Destroy(temp, 15f);
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnergyBall"|| other.tag == "SkySword")
        {

            cannonHealth--;
            if (cannonHealth == 0)
            {
                GameObject temp = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(this.gameObject);
                Destroy(temp,3f);
                
      
            }

        }
    }
}
