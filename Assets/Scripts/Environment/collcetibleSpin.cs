using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collcetibleSpin : MonoBehaviour
{
    public Vector3 myCenter;

    public float maxSpeed = 45;
    public float minSpeed = 0.0f;
    public float acceleration = 1f;
    public float curSpeed = 1f;
    public GameObject centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        myCenter = GetComponent<Renderer>().bounds.center;

        centerPoint = new GameObject("centerpt");
        centerPoint.transform.position = myCenter;

        centerPoint.transform.parent = this.gameObject.transform;


    }

    // Update is called once per frame
    void Update()
    {
        curSpeed += acceleration;

        if (curSpeed < minSpeed)
            curSpeed = minSpeed;
        else if (curSpeed > maxSpeed)
            curSpeed = maxSpeed;

        transform.RotateAround(centerPoint.transform.position, Vector3.up, curSpeed * Time.deltaTime);

    }
}
