using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TcpExample : MonoBehaviour
{
    private TcpClientConnection t;
    
    // Start is called before the first frame update
    void Start()
    {
       t = new TcpClientConnection();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            
            t.sendMessage();
        }
    }
}
