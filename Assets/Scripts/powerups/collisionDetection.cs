using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetection : MonoBehaviour
{
    Rigidbody rb;

    public GameObject control;
    public float force;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Control"))
            {
                control = transform.GetChild(i).gameObject;
            }
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.Contains("Level"))
        {
            foreach (ContactPoint cp in collision.contacts)
            {
                if (cp.thisCollider.name.Contains("Control"))
                {
                    rb.constraints = ~RigidbodyConstraints.FreezePosition;

                    // Calculate Angle Between the collision point and the player
                    Vector3 dir = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position) - transform.position;
                    // We then get the opposite (-Vector3) and normalize it
                    dir = -dir.normalized;
                    // And finally we add force in the direction of dir and multiply it by force. 
                    // This will push back the player
                    rb.AddForce(dir * force);
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //control
        if (other.gameObject.tag == "Refill" && control.GetComponent<control>().pointControl < control.GetComponent<control>().maxPointControl && gameObject.name.Contains("Control"))
        {
            control.GetComponent<control>().canRefill = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("Level"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //control
        if (other.gameObject.tag == "Refill" && gameObject.name.Contains("Control"))
        {
            control.GetComponent<control>().canRefill = false;
        }
    }

}
