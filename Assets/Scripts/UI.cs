using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Goal goal;

    private GameManager toolBoxManager;

    private Text currentStatsText;


    void Start()
    {
        toolBoxManager = GameManager.GetInstance();

        //TODO handle this respawn point
        currentStatsText = GameObject.Find("currentStats").GetComponent<Text>();
        //toolBoxManager.respawnPosition = GameObject.Find("RespawnPoint").transform.position; //get respawn point
        goal = GameObject.Find("Goal").GetComponent<Goal>();



      
    }

    // Update is called once per frame
    void Update()
    {
   
    }



    void updateStats()
    {
        
            currentStatsText.text = "keys: " + toolBoxManager.keys + "/" +
                                    goal.keysRequired + " ";
      
     
    }

    private void LateUpdate()
    {
        updateStats();
    }


   
}