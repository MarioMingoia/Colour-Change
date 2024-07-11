using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenPauseMenu : MonoBehaviour
{

    public GameObject pauseGameObjects;
    public MouseLookPlus mouseDisable;

    public GameObject hitMarker;

    public List<Material> toLerp;
    public GameObject panel;

    bool gamePaused = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        pauseGameObjects.SetActive(false);

        mouseDisable = GameObject.Find("Player").GetComponent<MouseLookPlus>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gamePaused == false)
            {
                showPausedMenu();
                UnlockCursor();

                panel.GetComponent<Image>().material = toLerp[gameObject.GetComponent<radial>().whotoChose];
                Time.timeScale = 0;
                gamePaused = true;

            }
            else if (gamePaused == true)
            {
                hidePausedMenu();
                LockCursor();
                Time.timeScale = 1;
                gamePaused = false;
            }
        }

        if (pauseGameObjects.activeInHierarchy == true)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }


    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void showPausedMenu()
    {
        mouseDisable.enabled = false;
        pauseGameObjects.SetActive(true);
        hitMarker.SetActive(false);
        GetComponent<radial>().enabled = false;
    }
    public void hidePausedMenu()
    {
        mouseDisable.enabled = true;
        pauseGameObjects.SetActive(false);
        hitMarker.SetActive(true);
        GetComponent<radial>().enabled = true;
    }

    public void resume()
    {
        hidePausedMenu();
        gamePaused = false;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1;
    }
}
