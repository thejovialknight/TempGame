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
    public Color selectedColor;
    public Color unselectedColor;
    public GameObject loadMenu;

    void Start() {
        index = 0; 
        newText.color = selectedColor;
        loadText.color = unselectedColor;
    }

    void Update()
    {
        if(Input.GetButtonDown("Up") || Input.GetButtonDown("Down"))
        {
            if (index == 0)
            {
                index = 1;
                newText.color = unselectedColor;
                loadText.color = selectedColor;
            } else if (index == 1)
            {
                index = 0;
                newText.color = selectedColor;
                loadText.color = unselectedColor;
            }
        }

        if(Input.GetButtonDown("Interact"))
        {
            if(index == 0)
            {
                GameManager.StartNewGame();
            }
            else
            {
                loadMenu.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
