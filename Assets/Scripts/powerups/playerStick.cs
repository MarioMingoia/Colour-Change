using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStick : MonoBehaviour
{

    public int myLayer;

    // Start is called before the first frame update
    void Start()
    {
        myLayer = transform.parent.gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //sticky

        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.gameObject.layer = 12;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        //sticky
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.gameObject.layer = myLayer;
        }
    }
}
