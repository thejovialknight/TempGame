using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sammy : NPC
{
    public GameObject mailSortObject;

    public Sammy()
    {
        id = "SAMMY";
        title = "Sammy";
        jobTitle = "Warehouse";
    }

    public override void OnDialogue(string id, string message, params string[] args)
    {
        base.OnDialogue(id, message);

        if(id != this.id) {
            return;
        }

        if(message == "OPEN") {
            DialogueManager.instance.Say("Are you a loser? You sure look like one.");
            DialogueManager.instance.AddOption("MINIGAME", id, "Got any work for me?");
            DialogueManager.instance.AddOption("CLOSE", id, "Okay, bye!");
            return;
        }

        if(message == "MINIGAME")
        {
            DialogueManager.instance.Close();
            mailSortObject.SetActive(true);
            MessageEventManager.MinigameStart("MAIL_SORT");
            return;
        }

        if (message == "CLOSE")
        {
            DialogueManager.instance.Close();
            return;
        }
    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
