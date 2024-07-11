using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class laserv2 : MonoBehaviour
{
    public Transform startPoint;
    public GameObject endPoint;
    private Vector3 oriendPoint;

    LineRenderer LaserLine;

    public LayerMask IgnoreMe;

    public float multiplyer;

    public GameObject endPoint2;
    int lengthOfLineRenderer = 2;

    public float distanceBetween;
    public float distanceBetweenReflect;

    [SerializeField] GameObject endObjectParticles, reflectPointArticles;
    void Start()
    {
        LaserLine = GetComponent<LineRenderer>();
        LaserLine.widthCurve = AnimationCurve.Linear(.25f, .25f,.25f, .25f);

        oriendPoint = endPoint.transform.localPosition;

        endPoint2.SetActive(false);

        distanceBetween = Vector3.Distance(startPoint.position, endPoint.transform.position);
        distanceBetweenReflect = distanceBetween / .5f;

        endObjectParticles = endPoint.transform.GetChild(0).gameObject;
        endObjectParticles.SetActive(false);
        reflectPointArticles = endPoint2.transform.GetChild(0).gameObject;
        reflectPointArticles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        LaserLine.SetPosition(0, startPoint.transform.position);
        LaserLine.SetPosition(1, endPoint.transform.position);
        endObjectParticles = endPoint.transform.GetChild(0).gameObject;
        if (endObjectParticles == null)
            Debug.Log(gameObject.name + " end object particles system is empty");
        reflectPointArticles = endPoint2.transform.GetChild(0).gameObject;
        if (reflectPointArticles == null)
            Debug.Log(gameObject.name + " reflect object particles system is empty");
        raycastStuff();
    }

    void raycastStuff()
    {
        Debug.DrawRay(startPoint.transform.position, transform.forward * distanceBetween, Color.green);

        Ray ray = new Ray(startPoint.transform.position, transform.forward * distanceBetween);
        //has the ray go from the front of the player
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanceBetween, ~IgnoreMe))
        {
            endPoint.transform.position = hit.point;
            endObjectParticles.SetActive(true);
            if (hit.collider.CompareTag("breakableWall"))
            {
                hit.transform.gameObject.GetComponent<MeshDestroy>().destroy = true;
            }

            if (hit.collider.name == "NoPowerup")
            {
                lengthOfLineRenderer = 3;
                LaserLine.positionCount = lengthOfLineRenderer;

                Vector3 inDirection = Vector3.Reflect(endPoint.transform.position - transform.position, hit.normal);
                //cast the reflected ray, using the hit point as the origin and the reflected direction as the direction  

                ray = new Ray(hit.point, inDirection * distanceBetweenReflect);
                Debug.DrawRay(hit.point, inDirection * distanceBetweenReflect, Color.black);

                LaserLine.SetPosition(2, endPoint.transform.position + inDirection);

                if (Physics.Raycast(ray, out hit, distanceBetweenReflect, ~IgnoreMe))
                {
                    endPoint2.SetActive(true);
                    reflectPointArticles.SetActive(true);
                    endPoint2.transform.position = hit.point;

                    lengthOfLineRenderer = 4;
                    LaserLine.positionCount = lengthOfLineRenderer;

                    LaserLine.SetPosition(3, endPoint2.transform.position);

                    if (hit.collider.CompareTag("breakableWall"))
                    {
                        hit.transform.gameObject.GetComponent<MeshDestroy>().destroy = true;
                    }

                }

                else
                {
                    lengthOfLineRenderer = 3;
                    LaserLine.positionCount = lengthOfLineRenderer;
                    endPoint2.SetActive(false);
                    reflectPointArticles.SetActive(false);
                }
            }

            else //not hit!  sanity check- if this fails, you may not have the collider component enabled.
            {
                lengthOfLineRenderer = 2;
                LaserLine.positionCount = lengthOfLineRenderer;
                endObjectParticles.SetActive(false);


            }
        }

        else
        {
            lengthOfLineRenderer = 2;
            LaserLine.positionCount = lengthOfLineRenderer;
            endPoint.transform.localPosition = oriendPoint;
            endObjectParticles.SetActive(false);

        }

    }
}


