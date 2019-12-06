using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    private Canon[] canon;
    private GameObject[] canonSpawns;
    public GameObject canonPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        canon = GameObject.FindObjectsOfType<Canon>();

        canonSpawns = GameObject.FindGameObjectsWithTag("CanonSpawn");
    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.T))
//        {
//            a();//TODO DEBUG
//        }
//    }

    private void OnTriggerEnter(Collider other)
    {
        
        //print("I GOT ENTERED");
        
        if (other.tag == "Character" && gameObject.name == "TriggerOne")
        {
        

            print("TriggerWorks");
            foreach (var c in canon)
            {
                c.isShooting = true;
            }

        }
        
        
        if (other.tag == "Character" && gameObject.name == "TriggerThree")
        {
            
            print("TriggerThree");

            foreach (var singleCanonSpawn in canonSpawns)
            {
                Instantiate(canonPrefab, singleCanonSpawn.transform.position, singleCanonSpawn.transform.rotation);
            }
        }
    }

//    void a()
//    {
//        print("TriggerWorks");
//        foreach (var c in canon)
//        {
//            c.isShooting = true;
//        }
//        
//    }
}
