using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstRoomPuzzles : MonoBehaviour
{
    [SerializeField] GameObject grappleComplete, stickyCoplete, moveComplete, pickupComplete;
    public bool completedPuzzle = false;

    [SerializeField] GameObject door;

    [SerializeField] GameObject lever;

    [SerializeField] GameObject wire;

    [SerializeField] Material activeColour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            completedPuzzle = true;

            lever.transform.localEulerAngles = new Vector3(0, 0, -50);
            wire.GetComponent<MeshRenderer>().material = activeColour;

            if (grappleComplete.GetComponent<firstRoomPuzzles>().completedPuzzle && stickyCoplete.GetComponent<firstRoomPuzzles>().completedPuzzle && moveComplete.GetComponent<firstRoomPuzzles>().completedPuzzle && pickupComplete.GetComponent<firstRoomPuzzles>().completedPuzzle)
            {
                door.GetComponent<doorInteraction>().locked = false;
            }
        }
    }
}
