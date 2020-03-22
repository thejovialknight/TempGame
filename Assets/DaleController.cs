using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaleController : MonoBehaviour
{
    DialogueManager dialogueManager;

    void OnEnable()
    {
        MessageEventManager.OnReceiveMessageEvent += OnReceiveMessage;
    }

    void OnDisable()
    {
        MessageEventManager.OnReceiveMessageEvent -= OnReceiveMessage;
    }

    void OnReceiveMessage(string id)
    {
        switch(id)
        {
            case "WAT001" :
                dialogueManager.Say("In what sense do you mean?");
                dialogueManager.AddOption(new DialogueOption("WAT002", "I don't get it."));
                break;
            case "WAT002":
                dialogueManager.Say("Well, I understand that.");
                dialogueManager.AddOption(new DialogueOption("WAT001", "Hello, there, what?"));
                break;
            default :
                break;
        }
    }

    void Start()
    {
        dialogueManager = DialogueManager.instance;
        dialogueManager.Say("Oi, there!");
        dialogueManager.AddOption(new DialogueOption("WAT001", "Hello, there, what?"));
        dialogueManager.AddOption(new DialogueOption("WAT002", "I don't get it."));
    }
}
