using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shimmy : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float speedZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Mathf.Cos(Time.time * 1) * speedX, Mathf.Cos(Time.time * 1) * speedY, Mathf.Cos(Time.time * 1) * speedZ);
    }
}
