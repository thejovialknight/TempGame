using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int index;
    public Text newText;
    public Text loadText;
    public GameObject loadMenu;

    void Update()
    {
        if(Input.GetButtonDown("Up") || Input.GetButtonDown("Down"))
        {
            if (index == 0)
            {
                index = 1;
            }

            if (index == 1)
            {
                index = 0;
            }
        }

        if(Input.GetButtonDown("Interact"))
        {
            if(index == 0)
            {
                SceneManager.LoadSceneAsync("Job_PostOffice");
            }
            else
            {
                loadMenu.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
