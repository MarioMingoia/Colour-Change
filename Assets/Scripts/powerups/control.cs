using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    // Start is called before the first frame update

    public bool letscontrol;
    public float pointControl;
    // add 10 to allow for error
    public float maxPointControl;

    public GameObject Player;

    public float blockcontroller;

    public bool canRefill = true;

    public int refillController;

    public KeyCode[] buttonKeys;
    public List<KeyCode> keysPressed;

    public bool amRiding;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        maxPointControl = pointControl + 20;

        refillController = 3;

        buttonKeys = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.UpArrow, KeyCode.RightShift ,KeyCode.RightControl};
    }

    // Update is called once per frame
    void Update()
    {

        if (letscontrol && pointControl > 0)
        {
            if (!amRiding)
            {
                transform.parent.Translate(Vector3.right * (Input.GetAxis("ControlX") * blockcontroller) * Time.deltaTime);
                transform.parent.Translate(Vector3.forward * (Input.GetAxis("ControlZ") * blockcontroller) * Time.deltaTime);
                transform.parent.Translate(Vector3.up * (Input.GetAxis("ControlY") * blockcontroller) * Time.deltaTime);


                for (int i = 0; i < buttonKeys.Length; i++)
                {
                    if (Input.GetKeyDown(buttonKeys[i]))
                    {
                        if (!keysPressed.Contains(buttonKeys[i]))
                        {
                            keysPressed.Add(buttonKeys[i]);
                        }
                    }

                    if (Input.GetKeyUp(buttonKeys[i]) && keysPressed.Count > 0)
                    {
                        for (int q = 0; q < keysPressed.Count; q++)
                        {
                            if (keysPressed[q] == buttonKeys[i])
                            {
                                keysPressed.RemoveAt(q);
                            }
                        }
                    }
                }

                pointControl -= keysPressed.Count;
            }

        }

        else
        {
            for (int i = 0; i < keysPressed.Count; i++)
            {
                keysPressed.RemoveAt(i);
            }
        }

        if (Input.GetKey(KeyCode.Q) || Player.GetComponent<CheckpointSystem>().reset == true || Player.GetComponent<CheckpointSystem>().isDead == true ||canRefill)
        {
            pointControl = maxPointControl;
            canRefill = false;
        }

    }
}
