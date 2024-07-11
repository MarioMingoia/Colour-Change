using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwanEndIn : MonoBehaviour
{
    // Start is called before the first frame update
    public int amountleft = 70;
    public GameObject spawnIn;

    private void Start()
    {
        spawnInObj();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EndPlat"))
        {
            if (amountleft > 0)
            {
                spawnInObj();
                other.GetComponent<checkVelocity>().enabled = true;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndPlat"))
        {
            other.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;
            other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<checkVelocity>().enabled = false;

        }
    }

    void spawnInObj()
    {
        GameObject abc = Instantiate(spawnIn, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        amountleft--;
        abc.name = abc.name.Replace("(Clone)", "");
        abc.GetComponent<checkVelocity>().enabled = false;
    }
}
