using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class radial : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject theMenu;

    public int whotoChose;

    public Vector2 moveInput;

    public Text[] options;

    public Color normalCol, highlightedCol;

    public GameObject highlight;

    public MouseLookPlus mlp;

    public GameObject pauseMenu;
    void Start()
    {
        theMenu.SetActive(false);

        mlp = GameObject.Find("Player").GetComponent<MouseLookPlus>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !pauseMenu.activeInHierarchy)
        {
            theMenu.SetActive(true);
            mlp.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }

        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            theMenu.SetActive(false);
            mlp.enabled = true;
        }

        if (theMenu.activeInHierarchy)
        {
            moveInput.x = Input.mousePosition.x - (Screen.width / 2);
            moveInput.y = Input.mousePosition.y - (Screen.height / 2);

            moveInput.Normalize();

            if (moveInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveInput.y, -moveInput.x) / Mathf.PI;
                angle *= 180;
                angle += 90f;
                if (angle < 0)
                {
                    angle += 360;
                }

                for (int i = 0; i < options.Length; i++)
                {
                    if (angle > i * (360 / options.Length) && angle < (i + 1) * (360 / options.Length))
                    {
                        options[i].color = highlightedCol;
                        whotoChose = i;

                        highlight.transform.rotation = Quaternion.Euler(0, 0, i * -(360 / options.Length));
                    }
                    else
                    {
                        options[i].color = normalCol;
                    }
                }
            }
        }
    }
}
