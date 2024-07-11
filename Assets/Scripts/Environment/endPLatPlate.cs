using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPLatPlate : MonoBehaviour
{
    // Start is called before the first frame update

    public Material firstMat;
    public Material activated;

    public GameObject pipe;
    public GameObject wire;
    public GameObject spawner;

    private void Awake()
    {
        spawner.SetActive(false);

    }
    void Start()
    {
        GetComponent<Renderer>().material = firstMat;
        pipe.GetComponent<Renderer>().material = firstMat;
        wire.GetComponent<Renderer>().material = firstMat;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "reflect")
        {
            GetComponent<Renderer>().material = activated;
            pipe.GetComponent<Renderer>().material = activated;
            wire.GetComponent<Renderer>().material = activated;
            spawner.SetActive(true);
        }
    }

}
