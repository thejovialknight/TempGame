using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour
{
    public Text interactNameText;
    public Text interactInfoText;

    void OnEnable()
    {
        MessageEventManager.OnSetInteractInfoEvent += OnSetInteractInfo;
        MessageEventManager.OnClearInteractInfoEvent += OnClearInteractInfo;
        MessageEventManager.OnPauseEvent += OnPause;
        //MessageEventManager.OnResumeEvent += OnResume;
    }

    void OnDisable()
    {
        MessageEventManager.OnSetInteractInfoEvent -= OnSetInteractInfo;
        MessageEventManager.OnClearInteractInfoEvent -= OnClearInteractInfo;
        MessageEventManager.OnPauseEvent -= OnPause;
        //MessageEventManager.OnResumeEvent -= OnResume;
    }

    void OnPause()
    {
        OnSetInteractInfo("", "");
    }

    void OnSetInteractInfo(string name, string info)
    {
        interactNameText.text = name;
        interactInfoText.text = info;
    }

    void OnClearInteractInfo()
    {
        interactNameText.text = "";
        interactInfoText.text = "";
    }
}
