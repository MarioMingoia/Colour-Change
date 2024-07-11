using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorInteraction : MonoBehaviour
{
    public bool locked;

    [SerializeField] bool isOpen = false;

    public Quaternion openAngleForwards;
    public Quaternion openAngleBackwards;
    [SerializeField] Quaternion OpemPos;

    public Quaternion closedAngle;

    public float timer;

    [SerializeField] GameObject door;

    public GameObject lockedUI;

    private void Awake()
    {

    }
    private void Start()
    {
        OpemPos = openAngleForwards;

        Debug.Log(lockedUI);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<CheckpointSystem>().reset = true;

            Vector3 Forward = door.transform.TransformDirection(Vector3.forward);
            Vector3 toOther = Vector3.Normalize(other.transform.position - door.transform.position);
            if (Vector3.Dot(Forward, toOther) < 0)
            {
                Debug.Log("forwards");
                OpemPos = openAngleForwards;

            }
            else
            {
                Debug.Log("backwards");
                OpemPos = openAngleBackwards;
            }

            if (!locked)
            {
                if (!isOpen)
                {
                    timer = 0;
                    StartCoroutine(doorMovement(closedAngle, OpemPos));
                }
            }
            else
            {
                lockedUI.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Door locked");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<CheckpointSystem>().reset = false;
            lockedUI.SetActive(false);

            if (!locked)
            {
                if (isOpen)
                {
                    timer = 0;
                    StartCoroutine(doorMovement(OpemPos, closedAngle));
                }
            }

        }
    }

    public IEnumerator doorMovement(Quaternion originalPos, Quaternion newPos)
    {
        yield return new WaitForFixedUpdate();

        timer += Time.deltaTime;
        door.transform.localRotation = Quaternion.Lerp(originalPos, newPos, timer);
        if (timer >= 1)
        {
            isOpen = !isOpen;
            yield break;
        }
        yield return doorMovement(originalPos, newPos);

    }
}
