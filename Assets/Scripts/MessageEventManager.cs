using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageEventManager : MonoBehaviour
{
    public delegate void TriggerEvent();
    public static event TriggerEvent OnJobRegister;
    public static void RegisterJob() {
        if(OnJobRegister != null) {
            OnJobRegister();
        }
    }

    public delegate void IDMessageEvent(string id, string message, params string[] args);

    public static event IDMessageEvent OnDialogue;
    public static void Dialogue(string id, string message, params string[] args)
    {
        if (OnDialogue != null)
        {
            OnDialogue(id, message, args);
        }
    }

    public static void Dialogue(string id, string message)
    {
        if (OnDialogue != null)
        {
            OnDialogue(id, message, null);
        }
    }

    public delegate void MessageEvent(string message);

    public static event MessageEvent OnCutscene;
    public static void Cutscene(string id)
    {
        if (OnCutscene != null)
        {
            OnCutscene(id);
        }
    }

    public static event MessageEvent OnMinigameStart;
    public static void MinigameStart(string id)
    {
        if (OnMinigameStart != null)
        {
            OnMinigameStart(id);
        }
    }

    public delegate void SelectEvent(string id, int index);

    public static event SelectEvent OnSelectEvent;
    public static void RaiseOnSelect(string id, int index)
    {
        if (OnSelectEvent != null)
        {
            OnSelectEvent(id, index);
        }
    }

    public delegate void InteractManagerEvent(string name, string info);

    public static event InteractManagerEvent OnSetInteractInfoEvent;
    public static void RaiseOnSetInteractInfo(string name, string info)
    {
        if (OnSetInteractInfoEvent != null)
        {
            OnSetInteractInfoEvent(name, info);
        }
    }

    public delegate void ClearEvent();

    public static event ClearEvent OnClearInteractInfoEvent;
    public static void RaiseOnClearInteractInfo()
    {
        if (OnClearInteractInfoEvent != null)
        {
            OnClearInteractInfoEvent();
        }
    }

    public delegate void ItemEvent(Item item);
    public static event ItemEvent OnSetActiveItem;
    public static void SetActiveItem(Item item) {
        if(OnSetActiveItem != null) {
            OnSetActiveItem(item);
        }
    }
}
