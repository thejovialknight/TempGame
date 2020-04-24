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
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "What do you think of...");
            DialogueManager.instance.AddOption("CLOSE", id, "Bye!");
            return;
        }

        #region INQUIRE_CHARACTER

        if(message == "INQUIRE_CHARACTER") {
            DialogueManager.instance.Say("...");
            DialogueManager.instance.AddOption("INQUIRE_SAMMY", id, "...Sammy?");
            DialogueManager.instance.AddOption("INQUIRE_HANK", id, "...Hank?");
            DialogueManager.instance.AddOption("INQUIRE_ARLENE", id, "...Arlene?");
            DialogueManager.instance.AddOption("OPEN", id, "< BACK");
            return;
        }

        if(message == "INQUIRE_SAMMY") {
            DialogueManager.instance.Say("Sammy doesn't like me very much. I suppose I don't like her very much either.");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");

            GameManager.instance.currentJob.flagCollection.SetFlag("KNOWLEDGE_SAMMY_DALE", true);
            return;
        }

        if(message == "INQUIRE_HANK") {
            DialogueManager.instance.Say("Hank's okay, I guess.");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if(message == "INQUIRE_ARLENE") {
            DialogueManager.instance.Say("Arlene's good.");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }


        #endregion

    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
