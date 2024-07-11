using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject belt;
    public List<string> tags;
    public Transform endPoint;
    public float speed;
    public GameObject find;

    //public CharacterController playerController;

    private void Start()
    {
        //playerController = GameObject.Find("Player").GetComponent<CharacterController>();
    }
    private void Update()
    {
    }
    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < tags.Count; i++)
        {
            if (other.gameObject.CompareTag(tags[i]))
            {
                other.transform.position = Vector3.MoveTowards(other.transform.position, endPoint.position, speed * Time.deltaTime);

                //playerController.enabled = false;
            }
        }

       

    }

    private void OnTriggerExit(Collider other)
    {
        //playerController.enabled = true;
    }
}
