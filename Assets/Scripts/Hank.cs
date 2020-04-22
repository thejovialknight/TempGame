using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hank : NPC
{
    public Hank()
    {
        id = "HANK";
        title = "Hank";
        jobTitle = "Delivery";
    }

    public override void OnDialogue(string id, string message, params string[] args)
    {
        base.OnDialogue(id, message);

        if(id != this.id) {
            return;
        }

        if (message == "OPEN")
        {
            DialogueManager.instance.Say("Hey, man. What's the what?");
            DialogueManager.instance.AddOption("CLOSE", id, "Nothing much.");
            return;
        }

        if(message == "CLOSE")
        {
            DialogueManager.instance.Close();
            return;
        }
    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
