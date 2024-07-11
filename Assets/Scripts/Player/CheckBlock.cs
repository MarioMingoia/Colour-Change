using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBlock : MonoBehaviour
{
    public Text whatPowerup;
    public Image colour;
    public Color baseCLR, sticky, grapple, control, pickup;

    public Color currentClr;
    public Text points;
    private Color32 pointOriginal;
    public GameObject radial;

    public Renderer objectRendererToChange;
    public GameObject raycastFrom;
    public float range;
    public Camera cam;

    public GameObject objectToControl;

    public LayerMask IgnoreMe;

    public GameObject pause;

    LineRenderer LaserLine;
    public GameObject endPoint;
    public GameObject startpt;
    private Vector3 oriendPoint;


    // Start is called before the first frame update
    void Start()
    {
        pointOriginal = points.color;
        //sets the original colour to return to
        oriendPoint = endPoint.transform.localPosition;

        LaserLine = GetComponent<LineRenderer>();
        LaserLine.widthCurve = AnimationCurve.Linear(.25f, .25f, .25f, .25f);
    }

    // Update is called once per frame
    void Update()
    {
        currentClr = colour.color;
        try
        {

            if (objectToControl.GetComponent<control>().pointControl <= 0)
            {
                points.color = Color.red;
                //changes the colour once the player has used all of the points for the yellow power up
                //the player has to move the yellow powerup within the point limit
            }

            if (objectToControl.GetComponent<control>().pointControl > 0)
            {
                points.color = pointOriginal;
                //keeps the colour the same
            }

            points.text = objectToControl.GetComponent<control>().pointControl.ToString();

            foreach (GameObject abc in GameObject.FindGameObjectsWithTag("SpecialBlocks"))
            {
                if (abc.GetComponent<Renderer>() != objectRendererToChange)
                {
                    abc.GetComponent<Renderer>().material.color = Color.white;

                    if (abc.transform.GetChild(4).gameObject.activeInHierarchy)
                    {
                        abc.transform.GetChild(4).gameObject.GetComponent<control>().letscontrol = false;
                    }
                }
            }
        }
        catch
        {

        }
       
        RaycastStuff();

        if (radial.activeInHierarchy)
        {
            if (pause.GetComponent<radial>().whotoChose == 0)
            {
                //this means that no powerups have been selected. shows white texture
                noPowerfct();
                //if player presses 0 - the text boxes font goes to normal. this means that no powerups have been selected

            }
            if (pause.GetComponent<radial>().whotoChose == 1)
            {
                //this means that a powerups have been selected. shows yellow texture
                controlPowerfct();
                //if player presses 1 - the 1st text boxes font goes to bold. this means that the 1st powerups have been selected

            }

            if (pause.GetComponent<radial>().whotoChose == 2)
            {
                //this means that a powerups have been selected. shows green texture
                stickPowerfct();

                //if player presses 2 - the 2nd text boxes font goes to bold. this means that the 2nd powerups have been selected

            }

            if (pause.GetComponent<radial>().whotoChose == 3)
            {
                //this means that a powerups have been selected. shows blue texture
                pickupPowerfct();
                //if player presses 3 - the 3rd text boxes font goes to bold. this means that the 3rd powerups have been selected

            }

            if (pause.GetComponent<radial>().whotoChose == 4)
            {
                //this means that a powerups have been selected. shows red texture
                grapplePowerfct();
                //if player presses 4 - the 4th text boxes font goes to bold. this means that the 4th powerups have been selected

            }
            lineStuff();
        }

        else if (!radial.activeInHierarchy)
        {
            LaserLine.enabled = false;
        }

    }

    void lineStuff()
    {
        LaserLine.enabled = true;
        LaserLine.startColor = currentClr;
        LaserLine.endColor = currentClr;
        LaserLine.SetPosition(0, startpt.transform.position);
        LaserLine.SetPosition(1, endPoint.transform.position);

    }
    void RaycastStuff()
    {

        Ray ray = new Ray(raycastFrom.transform.position, transform.forward);
        //has the ray go from the front of the player
        RaycastHit hit;

        try
        {
            if (Physics.Raycast(ray, out hit, range, ~IgnoreMe))
            {
                endPoint.transform.position = hit.point;
                if (hit.transform.tag == "SpecialBlocks")
                {
                    Debug.DrawRay(raycastFrom.transform.position, transform.forward * range, Color.red);


                    if (transform.childCount < 7)
                    {
                        if (objectRendererToChange == null)
                        {
                            objectRendererToChange = hit.transform.gameObject.GetComponent<Renderer>();
                        }

                        //highlight
                        if (hit.transform.gameObject.name == "NoPowerup")
                        {
                            objectRendererToChange.material.color = Color.gray;
                            points.enabled = false;
                            //changes the powerupblock to grey meaning that it has no powers
                        }
                        else if (hit.transform.name == "Control")
                        {
                            objectRendererToChange.material.color = Color.yellow;
                            objectToControl = hit.transform.GetChild(4).gameObject;
                            //the specific object is the one being controlled
                            objectToControl.GetComponent<control>().letscontrol = true;
                            //enables bool in another script to allow it to be controlled
                            points.enabled = true;
                            //enables point text box
                        }
                        else if (hit.transform.gameObject.name == "Sticky")
                        {
                            objectRendererToChange.material.color = Color.green;
                            points.enabled = false;
                        }
                        else if (hit.transform.gameObject.name == "Pull/Push")
                        {
                            objectRendererToChange.material.color = Color.blue;
                            points.enabled = false;
                        }
                        else if (hit.transform.gameObject.name == "Grapple")
                        {
                            objectRendererToChange.material.color = Color.red;
                            points.enabled = false;
                        }
                    }

                    //change powerup
                    if (radial.activeInHierarchy)
                    {
                        if (pause.GetComponent<radial>().whotoChose == 0)
                        {
                            //this means that no powerups have been selected. shows white texture
                            hit.transform.gameObject.name = "NoPowerup";
                            noPowerfct();
                            //if player presses 0 - the text boxes font goes to normal. this means that no powerups have been selected

                        }
                        if (pause.GetComponent<radial>().whotoChose == 1)
                        {
                            //this means that a powerups have been selected. shows yellow texture
                            hit.transform.gameObject.name = "Control";
                            controlPowerfct();
                            //if player presses 1 - the 1st text boxes font goes to bold. this means that the 1st powerups have been selected

                        }

                        if (pause.GetComponent<radial>().whotoChose == 2)
                        {
                            //this means that a powerups have been selected. shows green texture
                            hit.transform.gameObject.name = "Sticky";
                            stickPowerfct();

                            //if player presses 2 - the 2nd text boxes font goes to bold. this means that the 2nd powerups have been selected

                        }

                        if (pause.GetComponent<radial>().whotoChose == 3)
                        {
                            //this means that a powerups have been selected. shows blue texture
                            hit.transform.gameObject.name = "Pull/Push";
                            pickupPowerfct();
                            //if player presses 3 - the 3rd text boxes font goes to bold. this means that the 3rd powerups have been selected

                        }

                        if (pause.GetComponent<radial>().whotoChose == 4)
                        {
                            //this means that a powerups have been selected. shows red texture
                            hit.transform.gameObject.name = "Grapple";
                            grapplePowerfct();
                            //if player presses 4 - the 4th text boxes font goes to bold. this means that the 4th powerups have been selected

                        }
                    }
                }
            }
            else
            {
                Debug.DrawRay(raycastFrom.transform.position, transform.forward * range, Color.blue);

                objectRendererToChange.material.color = Color.white;
                objectRendererToChange = null;
                endPoint.transform.localPosition = oriendPoint;
                objectToControl.GetComponent<control>().letscontrol = false;
            }


        }
        catch
        {

        }

        
    }

    void noPowerfct()
    {
        whatPowerup.text = pause.GetComponent<radial>().whotoChose.ToString();
        colour.color = Color.Lerp(colour.color, baseCLR, Time.deltaTime / .25f);
    }

    void controlPowerfct()
    {
        whatPowerup.text = pause.GetComponent<radial>().whotoChose.ToString();
        colour.color = Color.Lerp(colour.color, control, Time.deltaTime / .25f);
    }

    void stickPowerfct()
    {
        whatPowerup.text = pause.GetComponent<radial>().whotoChose.ToString();
        colour.color = Color.Lerp(colour.color, sticky, Time.deltaTime / .25f);
    }

    void pickupPowerfct()
    {
        whatPowerup.text = pause.GetComponent<radial>().whotoChose.ToString();
        colour.color = Color.Lerp(colour.color, pickup, Time.deltaTime / .25f);
    }

    void grapplePowerfct()
    {
        whatPowerup.text = pause.GetComponent<radial>().whotoChose.ToString();
        colour.color = Color.Lerp(colour.color, grapple, Time.deltaTime / .25f);
    }
}

