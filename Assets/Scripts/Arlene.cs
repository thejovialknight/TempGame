using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arlene : NPC
{
    public Arlene()
    {
        id = "ARLENE";
        title = "Arlene";
        jobTitle = "General Manager";
    }

    public override void OnDialogue(string id, string message, params string[] args)
    {
        base.OnDialogue(id, message);

        if(id != this.id) {
            return;
        }

        if(message == "OPEN")
        {
            DialogueManager.instance.Say("Welcome to the job! Do you have any questions for me?");
            DialogueManager.instance.AddOption("INQUIRE_WORK", id, "Any work I can do?");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "> What do you think of...");
            DialogueManager.instance.AddOption("CLOSE", id, "Nope, see you later!");
            return;
        }

        if (message == "INQUIRE_WORK")
        {
            DialogueManager.instance.Say("I'm sure your fellow employees have a lot on their plate at the moment, you should go ask one of them.");
            DialogueManager.instance.AddOption("INQUIRE_WORK2", id, "...");
            return;
        }

        if (message == "INQUIRE_WORK2")
        {
            DialogueManager.instance.Say("I think Sammy mentioned she was falling a little behind on sorting mail today, maybe you should try your hand at that!");
            DialogueManager.instance.AddOption("OPEN", id, "...");
            return;
        }

        if (message == "INQUIRE_CHARACTER")
        {
            DialogueManager.instance.Say("...");
            DialogueManager.instance.AddOption("INQUIRE_DALE", id, "...Dale?");
            DialogueManager.instance.AddOption("INQUIRE_SAMMY", id, "...Sammy?");
            DialogueManager.instance.AddOption("INQUIRE_HANK", id, "...Hank?");
            DialogueManager.instance.AddOption("OPEN", id, "< BACK");
            return;
        }

        if (message == "INQUIRE_DALE")
        {
            DialogueManager.instance.Say("Dale is a hard worker and isn't quite as dull as he seems. He's not great with customers, though.");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if (message == "INQUIRE_SAMMY")
        {
            DialogueManager.instance.Say("Sammy is a bit intense, but she does her job quickly and efficiently, so I can't complain.");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if (message == "INQUIRE_HANK")
        {
            DialogueManager.instance.Say("Between you and me, Hank's a bit of a weirdo, but I suppose that's none of my business.");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");
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
