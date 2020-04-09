using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JobSelector : MonoBehaviour
{
    public Transform contentParent;
    public Transform jobSelectionPrefab;
    public int index;
    public Color selectedColor;
    public Color unselectedColor;

    void OnEnable() {
        foreach(Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        if(GameManager.manager.saveData.jobs.Length > 0)
        {
            foreach (JobData jobData in GameManager.manager.saveData.jobs)
            {
                if(jobData.isUnlocked) {
                    Transform jobObj = GameObject.Instantiate(jobSelectionPrefab, contentParent);
                    jobObj.GetComponent<Text>().text = jobData.name;
                }
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
            SceneManager.LoadSceneAsync(GameManager.manager.saveData.jobs[index].sceneName);
        }
    }
}
