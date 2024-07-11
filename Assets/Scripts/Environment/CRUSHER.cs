using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRUSHER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y < transform.position.y)
            {
                collision.gameObject.GetComponent<CheckpointSystem>().isDead = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpecialBlocks"))
        {
            GetComponent<gameMovingPamels>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SpecialBlocks"))
        {
            GetComponent<gameMovingPamels>().enabled = true;
        }
    }
}
