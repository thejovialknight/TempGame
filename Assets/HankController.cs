using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HankController : MonoBehaviour, IInteractable
{
    DialogueManager dialogueManager;

    public void InteractWith(Transform interactor)
    {
        MessageEventManager.RaiseOnReceiveMessage("HANK_OPEN");
    }

    public string GetInteractName()
    {
        return "Hank (Mail Man)";
    }

    public string GetInteractInfo()
    {
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
        switch (id)
        {
            case "HANK_OPEN":
                dialogueManager.Say("Hey man, you the new temp?.");
                dialogueManager.AddOption(new DialogueOption("HANK_END", "Do you think I could hitch a ride with you, you know, to "));
                break;
            case "HANK_END":
                dialogueManager.Close();
                break;
            default:
                break;
        }
    }

    void Start()
    {
        dialogueManager = DialogueManager.instance;
    }
}
