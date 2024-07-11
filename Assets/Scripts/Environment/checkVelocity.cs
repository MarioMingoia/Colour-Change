using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVelocity : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = ~RigidbodyConstraints.FreezeAll;

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity == Vector3.zero)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("EndSpawnPoint"))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }

        else
        {
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        }
    }
}
