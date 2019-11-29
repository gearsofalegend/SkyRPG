using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static int characterHealth = 3;

    public Vector3 respawnPosition;
    private Vector3 characterPosition;


    public float keys;

    public GameObject player;
    

    

    //toolbox variable 
    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            var gameObject = new GameObject("ToolBox");
            DontDestroyOnLoad(gameObject);
            _instance = gameObject.AddComponent<GameManager>();
        }

        return _instance;
    }


    void Awake()
    {
        if (_instance) //if not null destroy
        {
            Destroy(this);
        }

      
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
    public void CharacterRespawn(GameObject character)
    {
       // character.transform.position = respawnPosition;
       
    }
}
