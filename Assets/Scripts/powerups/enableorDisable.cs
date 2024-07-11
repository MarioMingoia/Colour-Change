using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableorDisable : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;

    [SerializeField] bool canChange;
    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.position;
        startRot = transform.rotation;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name.Contains(gameObject.name))
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        Destroy(GetComponent<MeshFilter>());
    }

    // Update is called once per frame
    void Update()
    {
        if (canChange == false)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.name.Contains(gameObject.name))
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }

                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        // reset
        if (Input.GetKey(KeyCode.Q) || GameObject.Find("Player").GetComponent<CheckpointSystem>().reset == true || GameObject.Find("Player").GetComponent<CheckpointSystem>().isDead == true)
        {
            this.transform.position = startPos;
            this.transform.rotation = startRot;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            this.transform.rotation = startRot;
        }
    }
}
