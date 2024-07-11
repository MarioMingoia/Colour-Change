using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkbellow : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.name == "Control")
        {
            if (other.transform.position.y < transform.position.y)
            {
                GameObject bellowMe = other.transform.GetChild(4).gameObject;
                bellowMe.GetComponent<control>().amRiding = true;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Control")
        {
            if (other.transform.position.y < transform.position.y)
            {
                GameObject bellowMe = other.transform.GetChild(4).gameObject;
                bellowMe.GetComponent<control>().amRiding = false;
            }
        }
    }
}
