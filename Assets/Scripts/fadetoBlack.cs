using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadetoBlack : MonoBehaviour
{
    float timer = 0;

    [SerializeField]
    GameObject p_Fade;
    [SerializeField]
    GameObject buttons;

    CanvasGroup cg_Fade;
    // Start is called before the first frame update
    void Start()
    {
        buttons.SetActive(false);
        cg_Fade = p_Fade.GetComponent<CanvasGroup>();
        cg_Fade.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float timer = 0;
            StartCoroutine(iefadetoBlack());

        }
    }

    IEnumerator iefadetoBlack()
    {
        yield return new WaitForFixedUpdate();
        timer += Time.deltaTime;
        cg_Fade.alpha += Time.deltaTime;
        if (timer >= 1)
        {
            buttons.SetActive(true);
            Time.timeScale = 0;
            yield break;
        }

        yield return iefadetoBlack();

    }
}
