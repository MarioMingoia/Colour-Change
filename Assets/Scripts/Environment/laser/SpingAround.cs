using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpingAround : MonoBehaviour
{
    public Vector3 myCenter;

    public float maxSpeed = 45;
    public float minSpeed = 0.0f;
    public float acceleration = 1f;
    public float curSpeed = 1f;

    public bool increase;

    public GameObject centerPoint;
    // Start is called before the first frame update
    void Start()
    {
        myCenter = GetComponent<Renderer>().bounds.center;

        centerPoint = new GameObject("centerpt");
        centerPoint.transform.position = myCenter;

        centerPoint.transform.parent = this.gameObject.transform;

        increase = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(increase)
            curSpeed += acceleration;

        if (curSpeed < minSpeed)
            curSpeed = minSpeed;
        else if (curSpeed > maxSpeed)
            curSpeed = maxSpeed;

        transform.RotateAround(centerPoint.transform.position, Vector3.up, curSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Stopper" || other.gameObject.name == "Sticky")
        {
            increase = false;

            curSpeed -= 1;
            if (curSpeed < minSpeed)
                curSpeed = minSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Stopper" || other.gameObject.name == "Sticky")
        {
            increase = true;
        }
    }
}
