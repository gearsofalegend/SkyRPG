using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Canon[] canon;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.name == "TriggerOne")
        {
            //canon.isShooting = true;

            foreach (var c in canon)
            {
                c.isShooting = true;
            }

        }
    }
}
