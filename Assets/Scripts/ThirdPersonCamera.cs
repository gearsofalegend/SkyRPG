using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{


    public GameObject targetToRotateAround;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Character");
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        
    }


    void CameraMovement()
    {
        
        float mouseHorizontal = Input.GetAxis("Mouse X");
        float mouseVertical = Input.GetAxis("Mouse Y");
        
        transform.RotateAround(targetToRotateAround.transform.position, Vector3.up, mouseHorizontal * Time.deltaTime);
        
        player.transform.Rotate(Vector3.up,mouseHorizontal * 100 * Time.deltaTime);
        
    }
}
