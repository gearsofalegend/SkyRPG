using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuideArrow : MonoBehaviour
{
    
    private Dictionary<float, Crate> crateDictionary;
    private Transform characterTransform;
    private Transform nearbyCrateTransform;
    
    public MeshRenderer[] arrowMeshRenderers;// what render the object in scene
    private bool isVisible;
    
    // Start is called before the first frame update
    void Start()
    {
        crateDictionary = new Dictionary<float, Crate>();
        
        
        characterTransform = GameObject.FindWithTag("Character").transform;

        nearbyCrateTransform = GetNearbyCrate();
        
        InvokeRepeating("assignNearby",0,0.1f);

      

    }

    // Update is called once per frame
    void Update()
    {

        
            transform.LookAt(nearbyCrateTransform);
            HideArrowMethod();
            //transform.position = characterTransform.GetChild(0).position;//ArrowAnchor GameObject
        
      

      
    }


    
    public Transform GetNearbyCrate()
    {
        Transform temp = null; //prepare Transform
       crateDictionary.Clear();//delete dictionary entries
      // crateDictionary = null;
      // crateDictionary = new Dictionary<float, Crate>();

        // getCrates GameObject
        Crate[] crates = FindObjectsOfType<Crate>();
        
        
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

 

    void HideArrowMethod() //things learned: use else if instead for enabling and disabling
    {
        
        if (Input.GetKeyDown(KeyCode.H) && isVisible)
        {

            print("what up's");
            foreach (var singleArrowRenderer in arrowMeshRenderers)
            {
                singleArrowRenderer.enabled = false;
            }

            isVisible = false;            

        }else if (Input.GetKeyDown(KeyCode.H) && !isVisible)
        
        {

            foreach (var singleArrowRenderer in arrowMeshRenderers)
            {
                singleArrowRenderer.enabled = true;
                print("Info "+ singleArrowRenderer.enabled);
            }

            isVisible = true;

        }
    }


}
