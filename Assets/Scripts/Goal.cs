using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public float keysRequired; 
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
       gameManager = GameManager.GetInstance();

       keysRequired = 3;
       SSTools.ShowMessage("Get Keys & Reach the Goal",SSTools.Position.top,SSTools.Time.threeSecond);
       
    }


    void OnTriggerEnter(Collider other) 
    {
        if (gameManager.keys == keysRequired && other.tag == "Character")
        {
            //toolBoxManager.keys = 0; //restart key count
            print("DOOR UNLOCK");
            SSTools.ShowMessage("GameOver",SSTools.Position.top,SSTools.Time.threeSecond);
        }
        else if(other.tag == "Character")
        {
            SSTools.ShowMessage((keysRequired - gameManager.keys) + "KEYS missing!! ",SSTools.Position.top,SSTools.Time.threeSecond);

        }


    }
    
    
    
}
