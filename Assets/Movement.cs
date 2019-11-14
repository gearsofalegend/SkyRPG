using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        MoveCube(moveX,moveY, Time.deltaTime * 5.0f);

    }



    void MoveCube(float x, float z, float speed = 5.0f)
    {
        //
        transform.Translate(new Vector3(x * speed , 0, z * speed));
        
        
    }
    
}
