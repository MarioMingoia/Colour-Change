using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public GameObject slidingObj, door;
    // Start is called before the first frame update

    public GameObject wire;
    public Material activatedMaterial;

    public string chooseTag;

    float timer;

    public bool cantSlide, openDoor;

    public GameObject positionPt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains(chooseTag))
        {
            if (cantSlide == false)
            {
                timer = 0;
                if (slidingObj != null && positionPt != null)
                    StartCoroutine(risingMovement(positionPt.transform.position, slidingObj.transform.position));
            }
            if (openDoor == true)
            {
                if (door != null)
                    door.GetComponent<doorInteraction>().locked = false;
            }

             if (wire != null && activatedMaterial != null)
                wire.GetComponent<Renderer>().material = activatedMaterial;
            GetComponent<Renderer>().material = activatedMaterial;
        }


    }

    IEnumerator risingMovement(Vector3 originalPos, Vector3 newPos)
    {
        yield return new WaitForFixedUpdate();

        timer += Time.deltaTime;
        slidingObj.transform.position = Vector3.Lerp(originalPos, newPos, timer);
        if (timer >= 1)
        {
            yield break;
        }
        yield return risingMovement(originalPos, newPos);

    }
}
