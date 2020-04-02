using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class LoadMenuController : MonoBehaviour
{
    public Transform contentParent;
    public Transform loadFilePrefab;
    public int index;
    public Color selectedColor;
    public Color unselectedColor;

    void OnEnable() {
        foreach(Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        if (Directory.Exists(Path.Combine(Application.persistentDataPath, "saves")))
        {
            foreach (string filename in Directory.GetFiles(Path.Combine(Application.persistentDataPath, "saves")))
            {
                Transform loadObj = GameObject.Instantiate(loadFilePrefab, contentParent);
                loadObj.GetComponent<Text>().text = Path.GetFileName(filename);
            }

            SetSelection(0);
        }
    }

    void SetSelection(int newIndex) {
        index = newIndex;

        if(index < 0) {
            index = contentParent.childCount - 1;
        }
        if(index > contentParent.childCount - 1) {
            index = 0;
        }

        foreach(Transform child in contentParent)
        {
            child.GetComponent<Text>().color = unselectedColor;
        }

        contentParent.GetChild(index).GetComponent<Text>().color = selectedColor;
    }

    void Update()
    {
        if(Input.GetButtonDown("Up"))
        {
            SetSelection(index - 1);
        }

        if(Input.GetButtonDown("Down"))
        {
            SetSelection(index + 1);
        }

        if(Input.GetButtonDown("Interact"))
        {
            GameManager.manager.filename = contentParent.GetChild(index).GetComponent<Text>().text;
            GameManager.manager.LoadFile(GameManager.manager.filename);
        }
    }
}
