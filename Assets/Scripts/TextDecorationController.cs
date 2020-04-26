using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDecorationController : MonoBehaviour, IInteractable
{
    public string decoInfo = "Decoration Info Here";
    public string decoName = "Decoration Name";
    public string decoDescription = "It's a decoration.";
    public string id = "MSGID";

    void OnEnable()
    {
        MessageEventManager.OnDialogue += OnDialogue;
    }

    void OnDisable()
    {
        MessageEventManager.OnDialogue -= OnDialogue;
    }

    public void InteractWith(Transform interactor)
    {
        MessageEventManager.Dialogue(id, "OPEN");
    }

    public string GetInteractName()
    {
        return decoName;
    }

    public string GetInteractInfo()
    {
        return decoInfo;
    }

    void OnDialogue(string id, string message, params string[] args)
    {
        if(id != this.id) {
            return;
        }

        if(message == "OPEN") {
            DialogueManager.Say(decoDescription);
            DialogueManager.AddOption(new DialogueOption("END", id, "..."));
        }
        else if(message == "END") {
            DialogueManager.Close();
        }
    }
}
