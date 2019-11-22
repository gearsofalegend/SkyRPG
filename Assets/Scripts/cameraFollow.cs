using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float damping;
    private float startime;
    private float elapseTime;
    public float speed; 
    
    
    public Transform targetCameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        startime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        elapseTime = (Time.time - startime) * damping;


        //        transform.position = Vector3.Lerp(transform.position, targetCameraPosition.position, damping	* Time.deltaTime);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, targetCameraPosition.rotation, damping	* Time.deltaTime);


        //      transform.position = Vector3.Lerp(transform.position, targetCameraPosition.position, elapseTime	);
        //      transform.rotation = Quaternion.Slerp(transform.rotation, targetCameraPosition.rotation, elapseTime	);


        // go toward the enemy
        Vector3 directionToPlayer = targetCameraPosition.position - transform.position;

        if(Vector3.Magnitude(directionToPlayer) > 1)
        {
            transform.position += directionToPlayer.normalized * Time.deltaTime * 10;
        }
        else
        {
            transform.position = targetCameraPosition.position;
        }
    }
}
