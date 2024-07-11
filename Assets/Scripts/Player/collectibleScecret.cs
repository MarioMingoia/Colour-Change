using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectibleScecret : MonoBehaviour
{
    // Start is called before the first frame update
    public Text secretCollected;

    public int amountICollected;
    public int allSecrets;

    void Start()
    {
        amountICollected = 0;
        allSecrets = GameObject.FindGameObjectsWithTag("collectSecret").Length;
    }

    // Update is called once per frame
    void Update()
    {
        secretCollected.text = ("Secrets: " + amountICollected + "/" + allSecrets);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectSecret"))
        {
            amountICollected++;
            Destroy(other.gameObject);
        }
    }
}
