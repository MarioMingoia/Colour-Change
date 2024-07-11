using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Grapple : MonoBehaviour
{
	public Camera cam;
    public float grappleDist;

    public bool doGrapple = false;

    public float speed;

    private Vector3 grapplePos;

    //private Rigidbody myBody;

    public CharacterController cc_Controller;

    public float countdown;
    public float myCountdown;

    public GameObject grappleObject;

    public GameObject raycastFrom;
    public Vector3 originalScale;

    private void Start()
    {
        //myBody = GetComponent<Rigidbody>();
        cc_Controller = GetComponent<CharacterController>();
        myCountdown = countdown;
        grappleObject.SetActive(false);

        originalScale = grappleObject.transform.localScale;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DoGrappleRay();
            countdown = myCountdown;
            grappleObject.SetActive(false);
        }


        if (doGrapple == true)
        {
            GrappletoPos();
            CheckDist();
        }
    }

    private void GrappletoPos()
    {
        transform.position = Vector3.Lerp(transform.position, grapplePos, speed * Time.deltaTime);
    }

    private void CheckDist()
    {
        grappleObject.SetActive(true);
        float dist = Vector3.Distance(transform.position, grapplePos);
        grappleObject.transform.localScale = new Vector3(grappleObject.transform.localScale.x, grappleObject.transform.localScale.y, dist);
        if (dist <= 0.5f)//--1f should should not be hard coded like this.
        {

            grappleObject.SetActive(false);
            grappleObject.transform.localScale = originalScale;

            if (countdown >= 0)
            {
                countdown -= Time.deltaTime;             
            }
            if (countdown <= 0)
            {
                cc_Controller.enabled = true;
                doGrapple = false;
                //myBody.isKinematic = false;
            }
        }
    }

    private void DoGrappleRay()
    {
        Ray ray = new Ray(raycastFrom.transform.position, transform.forward);
        RaycastHit hit;
       if(Physics.Raycast(ray, out hit, grappleDist) && hit.transform.gameObject.name == "Grapple")
        {
            grapplePos = hit.point;
            doGrapple = true;
            //myBody.isKinematic = true;
            cc_Controller.enabled = false;
        }
    }
}
