using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventManager : MonoBehaviour
{
    public delegate void MessageEvent(string msg);

    public static event MessageEvent OnSayEvent;
    public static void RaiseOnSay(string msg)
    {
        if (OnSayEvent != null)
        {
            OnSayEvent(msg);
        }
    }

    public delegate void OptionEvent(string id, string msg);
    public static event OptionEvent OnAddOptionEvent;
    public static void RaiseOnAddOption(string id, string msg)
    {
        if (OnAddOptionEvent != null)
        {
            OnAddOptionEvent(id, msg);
        }
    }
}
