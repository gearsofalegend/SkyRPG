using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticle : MonoBehaviour
{

    public Transform destination;

    public float speed = 1f;

    private Vector3 originalPosition;
    private float counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        this.originalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.counter += Time.deltaTime * this.speed;
        this.counter = Mathf.Clamp01(this.counter);
        this.transform.position = Vector3.Lerp(this.originalPosition, this.destination.position, 1-this.counter);
    }
}
