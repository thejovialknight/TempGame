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

    public override void OnDialogue(string id, string message, params string[] args)
    {
        base.OnDialogue(id, message);

        if(id != this.id) {
            return;
        }

        if(message == "OPEN")
        {
            DialogueManager.instance.Say("Hey.");
            DialogueManager.instance.AddOption("CLOSE", id, "Bye!");
            return;
        }

        if(message == "CLOSE") {
            DialogueManager.instance.Close();
            return;
        }
    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
