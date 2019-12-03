using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    //  private GuideArrow arrow;
    public GameObject key;


    // Start is called before the first frame update
    void Start()
    {
        // arrow = GameObject.FindObjectOfType<GuideArrow>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SkySword")
        {
            Instantiate(key, transform.position, transform.rotation);
            Destroy(this.gameObject);

            //TODO instantiate Key or Item
        }
    }

//    private void OnDestroy()
//    {
//        //transform.SendMessage("GetNearbyCrate", SendMessageOptions.DontRequireReceiver);
//        arrow.GetNearbyCrate();
//    }
}