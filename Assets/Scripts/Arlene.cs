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

    public override void HandleDialogue(string message, params string[] args)
    {
        if(message == "OPEN")
        {
            if(DialogueManager.CheckArg(args, 0) == "NEVERMIND") 
            {
                DialogueManager.instance.Say("Okay, then.");
            }
            else 
            {
                string introduction = "";
                if(!flagCollection.CheckFlag("INTRODUCED"))
                {
                    introduction += "Welcome to the job! ";
                    flagCollection.SetFlag("INTRODUCED", true);
                }

                DialogueManager.instance.Say(introduction + "Do you have any questions for me?");
            }

            DialogueManager.instance.AddOption("INQUIRE_WORK", id, "Any work I can do?");

            if(!GameManager.instance.currentJob.flagCollection.CheckFlag("KNOWLEDGE_PACKAGE"))
            {
                DialogueManager.instance.AddOption("HUB_PACKAGE", id, "How are you doing?");
            }
            else if(GameManager.instance.currentJob.flagCollection.CheckFlag("KNOWLEDGE_PACKAGE"))
            {
                DialogueManager.instance.AddOption("HUB_PACKAGE", id, "About the missing package...", "QUESTIONS");
            }

            if(GameManager.instance.currentJob.flagCollection.CheckFlag("KNOWLEDGE_SAMMY_DALE"))
            {
                DialogueManager.instance.AddOption("HUB_DOWNSIZING", id, "What's the beef between Sammy and Dale?");
            }

            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "What do you think of...");
            DialogueManager.instance.AddOption("CLOSE", id, "[X] Nope, see you later!");
            return;
        }

        #region HUB_PACKAGE

        if (message == "HUB_PACKAGE")
        {
            if (DialogueManager.CheckArg(args, 0) == "QUESTIONS")
            {
                DialogueManager.instance.Say("Okay, shoot.");
            }
            else if (DialogueManager.CheckArg(args, 0) == "HUB")
            {
                DialogueManager.instance.Say("Anything else?");
            }
            else
            {
                GameManager.instance.currentJob.flagCollection.SetFlag("KNOWLEDGE_PACKAGE", true);
                DialogueManager.instance.Say("I've been better, to tell you the truth. I'm dealing with a missing package that's apparently pretty valuable.");
            }

            if(GameManager.instance.currentJob.flagCollection.CheckStringFlag("QUEST_PACKAGE") == null) {
                DialogueManager.instance.AddOption("INQUIRE_PACKAGE_HELP", id, "Anything I can do?");
            }

            DialogueManager.instance.AddOption("INQUIRE_PACKAGE_CONTENT", id, "What's in the box?");
            DialogueManager.instance.AddOption("OPEN", id, "< BACK");
            return;
        }

        if (message == "INQUIRE_PACKAGE_CONTENT")
        {
            DialogueManager.instance.Say("I'm not really allowed to say, but I can tell you it's a pain in my ass!");
            DialogueManager.instance.AddOption("HUB_PACKAGE", id, "Got it.", "HUB");
            DialogueManager.instance.AddOption("INQUIRE_PACKAGE_CONTENT_2", id, "What's in the box?? What's in the box!?");
            return;
        }

        if (message == "INQUIRE_PACKAGE_CONTENT_2")
        {
            flagCollection.SetFlag("PLAYER_IS_ODD", true);
            DialogueManager.instance.Say("Hey, are you alright?");
            DialogueManager.instance.AddOption("WHATS_BOX_YES", id, "Yes, I'm perfectly fine, why do you ask?");
            DialogueManager.instance.AddOption("WHATS_BOX_NO", id, "No, not really.");
            return;
        }

        if (message == "WHATS_BOX_YES")
        {
            DialogueManager.instance.Say("Oh, uh, nothing. You just seemed a little... nevermind.");
            DialogueManager.instance.AddOption("HUB_PACKAGE", id, "Okay.", "HUB");
            return;
        }

        if (message == "WHATS_BOX_NO")
        {
            DialogueManager.instance.Say("You know, you're a bit of a weird dude.");
            DialogueManager.instance.AddOption("HUB_PACKAGE", id, "Yes.", "HUB");
            return;
        }

        if (message == "INQUIRE_PACKAGE_HELP")
        {
            DialogueManager.instance.Say("If you could try gathering some information from the others that would probably help me get to the bottom of this.");
            DialogueManager.instance.AddOption("ACCEPT_PACKAGE_HELP", id, "Yes, ma'am.");
            DialogueManager.instance.AddOption("REJECT_PACKAGE_HELP", id, "Not my problem, sorry.");
            DialogueManager.instance.AddOption("HUB_PACKAGE", id, "< I have some more questions first.", "QUESTIONS");
            return;
        }

        if (message == "ACCEPT_PACKAGE_HELP")
        {
            GameManager.instance.currentJob.flagCollection.SetStringFlag("QUEST_PACKAGE", "IN_PROGRESS");
            DialogueManager.instance.Say("Thanks a lot!");
            DialogueManager.instance.AddOption("HUB_PACKAGE", id, "No problem.", "HUB");
            return;
        }

        if (message == "REJECT_PACKAGE_HELP")
        {
            GameManager.instance.currentJob.flagCollection.SetStringFlag("QUEST_PACKAGE", "REJECTED");
            DialogueManager.instance.Say("No need to be a dick, I wasn't asking for your help.");
            DialogueManager.instance.AddOption("OPEN", id, "...");
            return;
        }

        #endregion

        #region INQUIRE_WORK

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

        #endregion

        #region HUB_DOWNSIZING

        if(message == "HUB_DOWNSIZING") {
            DialogueManager.instance.Say("God, those two are a pain in my ass. I don't know what it is, but I'm sure Sammy started it.");

            // TAKE TO NEXT NODE WHERE SHE ASKS ABOUT FIRING
            DialogueManager.instance.AddOption("OPEN", id, "...");
        }

        #endregion

        #region INQUIRE_CHARACTER

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
            if(flagCollection.CheckFlag("PLAYER_IS_ODD")) {
                DialogueManager.instance.Say("Hank's a bit... odd. I'm sure you two woud get along nicely.");
            }
            else {
                DialogueManager.instance.Say("Between you and me, Hank's a bit of a weirdo, but I suppose that's none of my business.");
            }
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        #endregion

    }



    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
