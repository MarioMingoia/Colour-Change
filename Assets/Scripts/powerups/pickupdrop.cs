using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class pickupdrop : MonoBehaviour
{
    public bool buttonController;
    public GameObject Player;

    public Camera mainCamera;
    public GameObject pullorPushCamera;

    public GameObject pullControl;

    //positions transfer
    public Vector3 cameraStart;
    public Quaternion cameraStartRot;
    public Vector3 cameraEnd;
    public Quaternion cameraEndRot;

    public GameObject myParent;

    public Renderer myRenderer;
    public Renderer myRendererC1;
    public Renderer myRendererC2;
    public Renderer parentRenderer;
    public GameObject miniMi;

    // Start is called before the first frame update
    void Start()
    {
        Player = null;

        cameraStart = mainCamera.transform.localPosition;
        cameraStartRot = mainCamera.transform.localRotation;

        cameraEnd = pullorPushCamera.transform.localPosition;
        cameraEndRot = pullorPushCamera.transform.localRotation;

        myParent = transform.parent.gameObject;

        myRenderer = GetComponent<Renderer>();

        myRendererC1 = transform.GetChild(0).GetComponent<Renderer>();
        myRendererC2 = transform.GetChild(1).GetComponent<Renderer>();
        parentRenderer = myParent.GetComponent<Renderer>();

        miniMi = transform.GetChild(2).gameObject;
        miniMi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (buttonController == true && Player != null)
                {
                    myParent.transform.parent = Player.transform;
                    myParent.GetComponent<enableorDisable>().enabled = false;
                    Player.GetComponent<CheckBlock>().enabled = false;
                    miniMi.SetActive(true);
                    parentRenderer.enabled = false;
                    myRendererC1.enabled = false;
                    myRenderer.enabled = false;
                    myRendererC2.enabled = false;
                    buttonController = false;
                }
                else if (buttonController == false || Player == null)
                {
                    myParent.transform.parent = null;
                    myParent.GetComponent<enableorDisable>().enabled = true;
                    Player.GetComponent<CheckBlock>().enabled = true;
                    miniMi.SetActive(false);
                    parentRenderer.enabled = true;
                    myRendererC1.enabled = true;
                    myRenderer.enabled = true;
                    myRendererC2.enabled = true;

                    buttonController = true;
                }
            }
        }
        catch 
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        //pickup
        if (other.gameObject.tag == "Player")
        {
            Player = other.gameObject;
            pullControl.SetActive(true);

            mainCamera.transform.localPosition = Vector3.Lerp(cameraEnd, cameraStart, Time.deltaTime);
            mainCamera.transform.localRotation = Quaternion.Lerp(cameraEndRot, cameraStartRot, Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!myParent.transform.parent == Player.transform)
            {
                Player = null;
                mainCamera.transform.localPosition = Vector3.Lerp(cameraStart, cameraEnd, Time.deltaTime);
                mainCamera.transform.localRotation = Quaternion.Lerp(cameraStartRot, cameraEndRot, Time.deltaTime);

                pullControl.SetActive(false);
            }


        }
    }

}
