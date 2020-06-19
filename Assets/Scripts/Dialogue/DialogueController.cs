using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueNode rootNode;

    public void StartDialogue() {
        RaiseOnSay(rootNode);
    }

    public delegate void DialogueNodeEvent(DialogueNode node);

    public static event DialogueNodeEvent OnSayEvent;
    public static void RaiseOnSay(DialogueNode node)
    {
        if (OnSayEvent != null)
        {
            OnSayEvent(node);
        }
    }
}
