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
            DialogueManager.Say("Hey.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "What do you think of...");
            DialogueManager.AddOption("CLOSE", id, "Bye!");
            return;
        }

        #region INQUIRE_CHARACTER

        if(message == "INQUIRE_CHARACTER") {
            DialogueManager.Say("...");
            DialogueManager.AddOption("INQUIRE_SAMMY", id, "...Sammy?");
            DialogueManager.AddOption("INQUIRE_HANK", id, "...Hank?");
            DialogueManager.AddOption("INQUIRE_ARLENE", id, "...Arlene?");
            DialogueManager.AddOption("OPEN", id, "< BACK");
            return;
        }

        if(message == "INQUIRE_SAMMY") {
            DialogueManager.Say("Sammy doesn't like me very much. I suppose I don't like her very much either.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");

            GameManager.JobFlags.SetFlag("KNOWLEDGE_SAMMY_DALE", true);
            return;
        }

        if(message == "INQUIRE_HANK") {
            DialogueManager.Say("Hank's okay, I guess.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if(message == "INQUIRE_ARLENE") {
            DialogueManager.Say("Arlene's good.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }


        #endregion

    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
