using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableCrusherPlate : MonoBehaviour
{
    // Start is called before the first frame update

    public Material firstMat;
    public Color myColor;

    public GameObject crusher;
    void Start()
    {
        firstMat = GetComponent<Renderer>().material;
        myColor = firstMat.color;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "reflect")
        {
            crusher.GetComponent<enableCrusher>().emit = true;
            firstMat.SetColor("_EMISSION", myColor);
            firstMat.EnableKeyword("_EMISSION");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("reflect"))
        {
            crusher.GetComponent<enableCrusher>().emit = false;
            firstMat.SetColor("_EMISSION", myColor);
            firstMat.DisableKeyword("_EMISSION");
        }
    }
}
