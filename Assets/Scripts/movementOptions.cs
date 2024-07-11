using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movementOptions : MonoBehaviour
{
    public List<GameObject> plrOptions;

    public int options;

    public Text optionNum;
    // Start is called before the first frame update
    void Start()
    {
        options = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            options -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            options += 1;
        }

        if (options >= plrOptions.Count)
        {
            options = 0;
        }
        
        if (options < 0)
        {
            options = plrOptions.Count - 1;
        }

        for (int i = 0; i < plrOptions.Count; i++)
        {
            if (plrOptions[i] == plrOptions[options])
            {
                plrOptions[i].SetActive(true);
            }
            else
            {
                plrOptions[i].SetActive(false);
            }
        }

        optionNum.text = ("Movement Option: " + (options + 1)).ToString();
    }
}
