using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaleController : MonoBehaviour, IInteractable
{
    DialogueManager dialogueManager;

    public void InteractWith(Transform interactor) {
        MessageEventManager.RaiseOnReceiveMessage("DALE_OPEN");
    }

    public string GetInteractInfo() {
        return "Dale (Front Counter)";
    }

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
            case "DALE_OPEN" :
                dialogueManager.Say("Hey.");
                dialogueManager.AddOption(new DialogueOption("DALE_HOWGOESIT", "Hi! How are you?"));
                dialogueManager.AddOption(new DialogueOption("DALE_INQUIREJOB", "What do you do here?"));
                break;
            case "DALE_HOWGOESIT" :
                dialogueManager.Say("Oh, you know.");
                dialogueManager.AddOption(new DialogueOption("DALE_OPEN", "Sure."));
                dialogueManager.AddOption(new DialogueOption("DALE_INQUIREOKAY", "You sure?"));
                break;
            case "DALE_INQUIREOKAY" :
                dialogueManager.Say("Not necessarily.");
                dialogueManager.AddOption(new DialogueOption("DALE_DEADEND", "Well, alright..."));
                break;
            case "DALE_INQUIREJOB":
                dialogueManager.Say("I'm front counter. It's fine, I guess.");
                dialogueManager.AddOption(new DialogueOption("DALE_INQUIREFINE", "Fine?"));
                break;
            case "DALE_INQUIREFINE":
                dialogueManager.Say("Yeah, it's fine. I get paid.");
                dialogueManager.AddOption(new DialogueOption("DALE_DEADEND", "Well, alright..."));
                break;
            case "DALE_DEADEND":
                dialogueManager.Say("...");
                dialogueManager.AddOption(new DialogueOption("DALE_OPEN", "..."));
                break;
            default :
                break;
        }
    }

    void Start()
    {
        dialogueManager = DialogueManager.instance;
    }
}
