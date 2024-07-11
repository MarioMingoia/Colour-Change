using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCollect : MonoBehaviour
{
    public GameObject checkifDestrtoyed;
    public GameObject plats;
    // Start is called before the first frame update
    void Start()
    {
        plats.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (!checkifDestrtoyed)
            {
                plats.SetActive(true);
            }
        }
        catch
        {
        }
    }
}
