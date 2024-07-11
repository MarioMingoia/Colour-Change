using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialPlate : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update

    public GameObject wire;
    public Material activatedMaterial;
    public Material originalMaterial;
    public Material plateoriginalMaterial;

    public Vector3 doorOriginalPos;

    public string chooseTag;

    public GameObject positionPt;
    void Start()
    {
        originalMaterial = wire.GetComponent<Renderer>().material;
        plateoriginalMaterial = GetComponent<Renderer>().material;
        doorOriginalPos = door.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains(chooseTag))
        {
            //reference door open + unlocking
            door.transform.position = Vector3.Lerp(positionPt.transform.position, door.transform.position, Time.deltaTime);

            wire.GetComponent<Renderer>().material = activatedMaterial;
            GetComponent<Renderer>().material = activatedMaterial;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains(chooseTag))
        {
            //reference door closing + locking
            door.transform.position = Vector3.Lerp(doorOriginalPos, door.transform.position, Time.deltaTime);

            wire.GetComponent<Renderer>().material = originalMaterial;
            GetComponent<Renderer>().material = plateoriginalMaterial;
        }
    }
}
