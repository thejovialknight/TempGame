using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageEventManager : MonoBehaviour
{
    public delegate void GenericEvent();
    public static event GenericEvent OnRegisterJobEvent;
    public static void RaiseRegisterJob() {
        if(OnRegisterJobEvent != null) {
            OnRegisterJobEvent();
        }
    }

    public delegate void PauseEvent(bool pausePlayer, bool pauseNPCs, params NPC[] exceptions);

    public static event PauseEvent OnPauseEvent;
    public static void RaiseOnPause(bool pausePlayer, bool pauseNPCs, params NPC[] exceptions)
    {
        if (OnPauseEvent != null)
        {
            OnPauseEvent(pausePlayer, pauseNPCs, exceptions);
        }
    }

    public static void RaiseOnPause(bool pausePlayer, bool pauseNPCs)
    {
        if (OnPauseEvent != null)
        {
            OnPauseEvent(pausePlayer, pauseNPCs, new NPC[0]);
        }
    }

    public delegate void ResumeEvent();

    public static event ResumeEvent OnResumeEvent;
    public static void RaiseOnResume()
    {
        if (OnResumeEvent != null)
        {
            OnResumeEvent();
        }
    }

    public delegate void MessageEvent(string id);

    public static event MessageEvent OnReceiveMessageEvent;
    public static void Broadcast(string id)
    {
        if (OnReceiveMessageEvent != null)
        {
            OnReceiveMessageEvent(id);
        }
    }

    public static void BroadcastCutscene(string id)
    {
        if (OnReceiveMessageEvent != null)
        {
            OnReceiveMessageEvent("CUTSCENE_" + id);
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
}
