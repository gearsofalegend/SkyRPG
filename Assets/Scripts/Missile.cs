using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       

        transform.LookAt(player.transform);

        // go toward the enemy
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer = directionToPlayer.normalized;

        transform.position += directionToPlayer * Time.deltaTime * 2;
    }
    
//    void OnCollisionEnter(Collision other)
//    {
//        if (other.gameObject.name == "EnergyBall")
//        {
//            Destroy(gameObject);
//        }
//    }
    
}