using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 RespawnPoint;
    public CharacterController cc_Reference_To_Character_Controller;

    public bool isDead, reset;

    private void Awake()
    {
        RespawnPoint = GameObject.Find("Player").transform.position;
    }
    void Start()
    {
        isDead = false;
        cc_Reference_To_Character_Controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
        {
            cc_Reference_To_Character_Controller.enabled = false;
            transform.position = RespawnPoint;
            cc_Reference_To_Character_Controller.enabled = true;
            isDead = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillPlayer" || other.gameObject.tag == "water")
        {
            isDead = true;
        }
        if (other.gameObject.tag == "Checkpoints")
        {
            reset = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Checkpoints")
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            RespawnPoint = other.gameObject.transform.GetChild(0).transform.position;

            reset = false;

        }
    }
}
