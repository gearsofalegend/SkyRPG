using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private GameObject player;
    public float missileSpeed;
    public GameObject burstExplosion;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Character");
    }

    // Update is called once per frame
    void Update()
    {
       

        transform.LookAt(player.transform);

        // go toward the enemy
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer = directionToPlayer.normalized;

        transform.position += directionToPlayer * Time.deltaTime * missileSpeed;
    }
    
    void OnTriggerEnter(Collider other)
    {
        
       // print("Sean Strike");
//        if (other.gameObject.tag == "EnergyBall")
//        { 
            
            Destroy(Instantiate(burstExplosion, transform.position, transform.rotation),1.5f);
            Destroy(gameObject);
       // }
    }
    
}