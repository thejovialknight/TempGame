using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractManager : MonoBehaviour
{
    public TextMeshProUGUI interactNameText;
    public TextMeshProUGUI interactInfoText;

    void OnEnable()
    {
        MessageEventManager.OnSetInteractInfoEvent += OnSetInteractInfo;
        MessageEventManager.OnClearInteractInfoEvent += OnClearInteractInfo;
        GameManager.OnPause += OnPause;
    }

    void OnDisable()
    {
        MessageEventManager.OnSetInteractInfoEvent -= OnSetInteractInfo;
        MessageEventManager.OnClearInteractInfoEvent -= OnClearInteractInfo;
        GameManager.OnPause -= OnPause;
    }

    void OnPause(bool pausePlayer, bool pauseNPCS, params NPC[] exceptions)
    {
        if(pausePlayer)
        {
            OnSetInteractInfo("", "");
        }
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
