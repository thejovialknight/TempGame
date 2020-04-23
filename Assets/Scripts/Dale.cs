using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dale : NPC
{
    public Dale()
    {
        id = "DALE";
        title = "Dale";
        jobTitle = "Front Counter";
    }

    public override void HandleDialogue(string message, string[] args)
    {
        if(message == "OPEN")
        {
            DialogueManager.instance.Say("Hey.");
            DialogueManager.instance.AddOption("CLOSE", id, "Bye!");
            return;
        }
    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
