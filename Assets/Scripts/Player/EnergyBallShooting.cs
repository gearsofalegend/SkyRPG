using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallShooting : MonoBehaviour
{
    //public KeyCode fireKey;
    public GameObject energyBlast;
    public Transform energyGeneratorTransform;
    private Animator animator;// for shooting animation

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        FireEnergyMethod();
    }



    GameObject temp;

    void FireEnergyMethod()//TODO cooldown
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            animator.SetBool("aim", true);
            
            temp = Instantiate(energyBlast, energyGeneratorTransform.position,energyGeneratorTransform.rotation);
            temp.GetComponent<SphereCollider>().enabled = false;
        }

        if (Input.GetButton("Fire1") && temp)
        {
            temp.transform.position = energyGeneratorTransform.transform.position;
            temp.transform.rotation = transform.rotation;
        }

        if (Input.GetButtonUp("Fire1") && temp)
        {
            animator.SetBool("aim", false);
            
            temp.GetComponent<SphereCollider>().enabled = true;
            temp.GetComponent<EnergyBlast>().isMoving = true;

            Destroy(temp, 2f);
        }
    }
    

}