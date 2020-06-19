using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventManager : MonoBehaviour
{
    public delegate void MessageEvent(DialogueNode node);

    public static event MessageEvent OnSayEvent;
    public static void RaiseOnSay(DialogueNode node)
    {
        if (OnSayEvent != null)
        {
            OnSayEvent(node);
        }
    }

    public delegate void OptionEvent(DialogueResponse response);
    public static event OptionEvent OnAddOptionEvent;
    public static void RaiseOnAddOption(DialogueResponse response)
    {
        if (OnAddOptionEvent != null)
        {
            OnAddOptionEvent(response);
        }
    }
}
