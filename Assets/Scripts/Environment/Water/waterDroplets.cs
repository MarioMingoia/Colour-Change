using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDroplets : MonoBehaviour
{
    public float length;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != gameObject.tag)
            Destroy(gameObject, length);
    }
}
