using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPlate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject waterDoor;
    public bool startFlowing;
    public bool startCD;
    public Material active;
    private Material original;

    public List<GameObject> cabels;
    public List<GameObject> pipes;

    public List<GameObject> waterPoint;

    public GameObject water;
    public  List<GameObject> waterSpawnedIn;
    public int amounttoSpawn;

    [SerializeField]
    float countodwn;
    public float myCountodwn;

    public float lengthDropletsLast;

    [SerializeField] GameObject secretWater;

    [SerializeField] GameObject drain;
    [SerializeField] Material closedDrain, openDrain;

    void Start()
    {
        drain.GetComponent<Renderer>().material = closedDrain;

        original = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (!startCD && !startFlowing)
        {
            //randomly choose if you can spawn in a water drop

            bool canSpawn = Random.value > 0.95;

            if (canSpawn)
            {
                bool spawnAtpipes = Random.value > 0.2;
                if (spawnAtpipes)
                {
                    int randomPipe = Random.Range(0, waterPoint.Count);
                    GameObject haveIspawned = Instantiate(water, new Vector3(waterPoint[randomPipe].transform.position.x, waterPoint[randomPipe].transform.position.y, waterPoint[randomPipe].transform.position.z), Quaternion.identity);
                    float scale = Random.Range(0, .5f);
                    haveIspawned.transform.localScale = new Vector3(scale, scale, scale);
                    haveIspawned.AddComponent<waterDroplets>();
                    haveIspawned.GetComponent<waterDroplets>().length = lengthDropletsLast;
                }
                else
                {
                    Debug.Log("spawn at secret");
                    GameObject haveIspawned = Instantiate(water, new Vector3(secretWater.transform.position.x, secretWater.transform.position.y, secretWater.transform.position.z), Quaternion.identity);
                    float scale = Random.Range(0, .5f);
                    haveIspawned.transform.localScale = new Vector3(scale, scale, scale);
                    haveIspawned.AddComponent<waterDroplets>();
                    haveIspawned.GetComponent<waterDroplets>().length = lengthDropletsLast;
                }
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (waterSpawnedIn.Count > 0 && !startFlowing && !startCD)
            {
                drain.GetComponent<Renderer>().material = openDrain;
                StartCoroutine(destroyWater());

                for (int i = 0; i < cabels.Count; i++)
                {
                    cabels[i].GetComponent<Renderer>().material = original;
                }
                for (int i = 0; i < pipes.Count; i++)
                {
                    pipes[i].GetComponent<Renderer>().material = original;
                }
                for (int i = 0; i < waterPoint.Count; i++)
                {
                    pipes[i].GetComponent<Renderer>().material = original;
                }
            }

            else
            {
                waterDoor.SetActive(true);
                //close the door by referencing the door ienumerator with the open backwards pos, closed pos and timer. keep door locked


                if (!startFlowing && !startCD)
                {
                    drain.GetComponent<Renderer>().material = closedDrain;

                    startCD = true;
                    countodwn = myCountodwn;
                    StartCoroutine(countdown());
                }
                GetComponent<Renderer>().material = active;

                for (int i = 0; i < cabels.Count; i++)
                {
                    cabels[i].GetComponent<Renderer>().material = active;
                }
                for (int i = 0; i < pipes.Count; i++)
                {
                    pipes[i].GetComponent<Renderer>().material = active;
                }
                for (int i = 0; i < waterPoint.Count; i++)
                {
                    pipes[i].GetComponent<Renderer>().material = active;
                }
            }

        }
    }

    IEnumerator destroyWater()
    {
        yield return new WaitForFixedUpdate();

        for (int i = 0; i < waterSpawnedIn.Count; i++)
        {
            waterSpawnedIn.RemoveAt(i);
            Destroy(waterSpawnedIn[i]);
        }

        if (waterPoint.Count <= 0)
        {
            drain.GetComponent<Renderer>().material = closedDrain;
            yield break;
        }

        yield return destroyWater();
    }

    IEnumerator countdown()
    {
        yield return new WaitForFixedUpdate();
        if (startCD && !startFlowing)
        {
            if (countodwn >= 0)
            {
                countodwn = Mathf.Clamp(countodwn, 0, countodwn);
                countodwn -= Time.deltaTime;
            }
            else
            {
                startCD = false;
                startFlowing = true;
                StartCoroutine(spawnWater());
                yield break;
            }


        }
        yield return countdown();
    }

    IEnumerator spawnWater()
    {
        yield return new WaitForFixedUpdate();

        if (startFlowing && !startCD)
        {
            for (int i = 0; i < amounttoSpawn; i++)
            {

                for (int q = 0; q < waterPoint.Count; q++)
                {
                    GameObject haveIspawned = Instantiate(water, new Vector3(waterPoint[q].transform.position.x, waterPoint[q].transform.position.y, waterPoint[q].transform.position.z), Quaternion.identity);

                    if (!waterSpawnedIn.Contains(haveIspawned))
                    {
                        waterSpawnedIn.Add(haveIspawned);
                    }
                }
            }

        }

        if (Mathf.Round(waterSpawnedIn.Count / 4) == amounttoSpawn)
        {
            startFlowing = false;

            waterDoor.SetActive(false);
            //open the door by referencing the door ienumerator with the closed pos, open backwards pos and timer. keep door locked

            yield break;
        }
        yield return spawnWater();
    }
}
