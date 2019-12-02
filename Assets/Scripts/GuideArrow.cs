using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuideArrow : MonoBehaviour
{
    //private Crate[] crates;
    private Dictionary<float, Crate> crateDictionary;
    private Transform characterTransform;
    private Transform nearbyCrateTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        //crates = GameObject.FindObjectsOfType<Crate>();
        crateDictionary = new Dictionary<float, Crate>();
        
        
        
//
//        foreach (var VARIABLE in c )
//        {
//            crateDictionary.Add();
//        }
        
        
        characterTransform = GameObject.FindWithTag("Character").transform;

        nearbyCrateTransform = GetNearbyCrate();
        
        InvokeRepeating("assignNearby",0,0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        
            transform.LookAt(nearbyCrateTransform);
        
      

//        if (Input.GetKeyDown(KeyCode.H) && !this.enabled)
//        {
//            gameObject.SetActive();
//        }


    }


    
    public Transform GetNearbyCrate()
    {
        Transform temp = null; //prepare Transform
       crateDictionary.Clear();//delete dictionary entries
      // crateDictionary = null;
      // crateDictionary = new Dictionary<float, Crate>();

        // getCrates GameObject
        Crate[] crates = GameObject.FindObjectsOfType<Crate>();
        
        
        foreach (var crate in crates)
        {           
            //add them to dictionary
            crateDictionary.Add(Vector3.Distance(characterTransform.position,crate.transform.position), crate);
        }


        try
        {
            
        float closestCrate = crateDictionary.ElementAt(0).Key;// get first element of dictionary value(float)

        foreach (var crate in crateDictionary)
                {
                    //float currentCrate = Vector3.Distance(characterTransform.position, crate.transform.position);
                    
                    if (crate.Key <= closestCrate)//TODO < or <=???
                    {
                        closestCrate = crate.Key;// update closestCrate
                        temp = crate.Value.transform;// update crate Transform
                    }
        
                }
        
        }
        catch (ArgumentOutOfRangeException e)
        {  
            print("MY EXCEPTION: " + e);
            return null;   
        }
        
        

        return temp;
    }

    void assignNearby()
    {
        nearbyCrateTransform = GetNearbyCrate();
    }

    private void LateUpdate()
    {
        //GetNearbyCrate();
    }
}
