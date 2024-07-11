using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableCrusher : MonoBehaviour
{
    public Color myColor;
    public Material mat;

    public bool emit;

    public float sizeoIncrease;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        myColor = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (emit)
        {
            mat.SetColor("_EMISSION", myColor);
            mat.EnableKeyword("_EMISSION");
            GetComponent<gameMovingPamels>().enabled = true;
        }
        else if (!emit)
        {
            mat.SetColor("_EMISSION", myColor);
            mat.DisableKeyword("_EMISSION");
            GetComponent<gameMovingPamels>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EndPlat"))
        {
            if (gameObject.name.Contains("x"))
            {
                if (other.transform.localScale.x <= sizeoIncrease)
                {
                    other.transform.localScale = new Vector3(sizeoIncrease, other.transform.localScale.y, other.transform.localScale.z);                    
                }
            }

            else if (gameObject.name.Contains("z"))
            {
                if (other.transform.localScale.z <= sizeoIncrease)
                {
                    other.transform.localScale = new Vector3(other.transform.localScale.x, other.transform.localScale.y, sizeoIncrease);
                }
            }
        }
    }
}
