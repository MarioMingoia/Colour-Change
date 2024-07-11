using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public Rigidbody rb;
    public float force ;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If the object we hit is the enemy
        if (other.gameObject.tag == "Player")
        {
            rb.constraints = ~RigidbodyConstraints.FreezeAll;
            rb.constraints = RigidbodyConstraints.FreezeRotationY;

            // Calculate Angle Between the collision point and the player
            Vector3 dir = other.GetComponent<Collider>().ClosestPointOnBounds(transform.position) - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            rb.AddForce(dir * force);
        }
    }
}
