using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MM_MovingObstacle : MonoBehaviour
{
    public List<GameObject> wayPoints;
    public int startingWayPoint = 0;
    public float movementSpeed = 3;
    private int currentWayPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentWayPoint = startingWayPoint;        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temporaryVector3 = Vector3.zero;
        temporaryVector3.x = wayPoints[currentWayPoint].transform.position.x;
        temporaryVector3.y = wayPoints[currentWayPoint].transform.position.y;
        temporaryVector3.z = wayPoints[currentWayPoint].transform.position.z;

        transform.LookAt(temporaryVector3);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, temporaryVector3) < movementSpeed / 4)
        {
            currentWayPoint++;
            if (currentWayPoint > wayPoints.Count - 1)
            {
                currentWayPoint = startingWayPoint;
            }
        }
    }

   


}
