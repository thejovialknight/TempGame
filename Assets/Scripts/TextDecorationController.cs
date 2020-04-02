using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDecorationController : MonoBehaviour, IInteractable
{
    public string decoInfo = "Decoration Info Here";
    public string decoName = "Decoration Name";
    public string decoDescription = "It's a decoration.";
    public string decoID = "MSGID";

    void OnEnable()
    {
        MessageEventManager.OnReceiveMessageEvent += OnReceiveMessage;
    }

    void OnDisable()
    {
        MessageEventManager.OnReceiveMessageEvent -= OnReceiveMessage;
    }

    public void InteractWith(Transform interactor)
    {
        MessageEventManager.Broadcast(decoID + "_OPEN");
    }

    public string GetInteractName()
    {
        return decoName;
    }

    public string GetInteractInfo()
    {
        return decoInfo;
    }

    void OnReceiveMessage(string id)
    {
        if(id == decoID + "_OPEN") {
            DialogueManager.instance.Say(decoDescription);
            DialogueManager.instance.AddOption(new DialogueOption(decoID + "_END", "..."));
        }
        else if(id == decoID + "_END") {
            DialogueManager.instance.Close();
        }
    }
}
