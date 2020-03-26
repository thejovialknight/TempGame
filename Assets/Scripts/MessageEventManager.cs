using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageEventManager : MonoBehaviour
{
    public delegate void PauseEvent();

    public static event PauseEvent OnPauseEvent;
    public static void RaiseOnPause()
    {
        if (OnPauseEvent != null)
        {
            OnPauseEvent();
        }
    }

    public static event PauseEvent OnResumeEvent;
    public static void RaiseOnResume()
    {
        if (OnResumeEvent != null)
        {
            OnResumeEvent();
        }
    }

    public delegate void MessageEvent(string id);

    public static event MessageEvent OnReceiveMessageEvent;
    public static void RaiseOnReceiveMessage(string id)
    {
        if (OnReceiveMessageEvent != null)
        {
            OnReceiveMessageEvent(id);
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
