using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Canon[] canon;
    
    // Start is called before the first frame update
    void Start()
    {
        canon = GameObject.FindObjectsOfType<Canon>();
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
            //canon.isShooting = true;

            print("TriggerWorks");
            foreach (var c in canon)
            {
                c.isShooting = true;
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
