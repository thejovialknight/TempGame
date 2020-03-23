using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArleneController : MonoBehaviour, IInteractable
{
    DialogueManager dialogueManager;

    public void InteractWith(Transform interactor) {
        MessageEventManager.RaiseOnReceiveMessage("ARLENE_OPEN");
    }

    public string GetInteractName()
    {
        return "Arlene (General Manager)";
    }

    public string GetInteractInfo() {
        return "Press E to talk";
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
            case "ARLENE_OPEN" :
                dialogueManager.Say("I'm real busy, sorry! Catch me later, okay?");
                dialogueManager.AddOption(new DialogueOption("ARLENE_BYE", "No problem! See ya!"));
                dialogueManager.AddOption(new DialogueOption("ARLENE_BYE2", "Okay, whatever."));
                break;
            case "ARLENE_BYE" :
                dialogueManager.Say("See ya!");
                dialogueManager.AddOption(new DialogueOption("ARLENE_END", "..."));
                break;
            case "ARLENE_BYE2" :
                dialogueManager.Say("No need for an attitude, smart guy.");
                dialogueManager.AddOption(new DialogueOption("ARLENE_END", "..."));
                break;
            case "ARLENE_END":
                dialogueManager.Close();
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
