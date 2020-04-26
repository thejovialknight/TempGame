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

    public override void HandleDialogue(string message, string[] args)
    {
        if(message == "OPEN") {
            DialogueManager.Say("Hey, you're the new guy, huh? You look a bit funny to me.");

            DialogueManager.AddOption("MINIGAME", id, "Got any work for me?");

            if(GameManager.JobFlags.CheckFlag("KNOWLEDGE_PACKAGE")) {
                DialogueManager.AddOption("MISSING_PACKAGE", id, "I have some questions about the missing package.");
            }
            else {
                DialogueManager.AddOption("MISSING_PACKAGE", id, "How's it going?");
            }

            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "What do you think of...");
            DialogueManager.AddOption("CLOSE", id, "Okay, bye!");
            return;
        }

        if(message == "MINIGAME")
        {
            DialogueManager.Close();
            mailSortObject.SetActive(true);
            MessageEventManager.MinigameStart("MAIL_SORT");
            return;
        }

        #region MISSING_PACKAGE

        if(message == "MISSING_PACKAGE") {
            if(!GameManager.JobFlags.CheckFlag("KNOWLEDGE_PACKAGE")) {
                DialogueManager.Say("Well, between Dale being a loser like always and now this missing package, I'd say things are just swell.");
                DialogueManager.AddOption("INQUIRE_DALE", id, "Dale?");
                DialogueManager.AddOption("INQUIRE_PACKAGE", id, "What missing package?");

                GameManager.JobFlags.SetFlag("KNOWLEDGE_PACKAGE", true);
                GameManager.JobFlags.SetFlag("KNOWLEDGE_SAMMY_DALE", true);
            }
            else {
                DialogueManager.Say("I'm all ears.");
            }

            if(GameManager.JobFlags.CheckStringFlag("PACKAGE_SAMMY_STATUS") != "ADMITTED") {
                DialogueManager.AddOption("INQUIRE_PACKAGE_BLAME", id, "Was it you who lost it?");
            }
            DialogueManager.AddOption("OPEN", id, "< BACK");
            return;
        }

        if(message == "INQUIRE_PACKAGE") {
            DialogueManager.Say("It was probably Dale. He's always making us all look bad.");
            GameManager.JobFlags.SetFlag("KNOWLEDGE_SAMMY_DALE", true);
            DialogueManager.AddOption("MISSING_PACKAGE", id, "...");
        }

        if(message == "INQUIRE_PACKAGE_BLAME") {
            DialogueManager.Say("It wasn't me! It was obviously Dale.");
            GameManager.JobFlags.SetFlag("KNOWLEDGE_SAMMY_DALE", true);
            DialogueManager.AddOption("PACKAGE_ACCUSE", id, "You're lying!");
            DialogueManager.AddOption("PACKAGE_ACQUIT", id, "Okay, I believe you.");
            DialogueManager.AddOption("MISSING_PACKAGE", id, "< I have more questions.");
            GameManager.JobFlags.SetStringFlag("PACKAGE_SAMMY_STATUS", "DENIED");
        }

        if(message == "PACKAGE_ACCUSE") {
            DialogueManager.Say("Who the hell do you think you are, new guy? I've worked here for 9 years! You've got nothing on me!");
            DialogueManager.AddOption("PACKAGE_NO_PROOF", id, "Yeah, that's all I have...");
        }

        if(message == "PACKAGE_NO_PROOF") {
            DialogueManager.Say("Just what I thought, loser.");
            DialogueManager.AddOption("MISSING_PACKAGE", id, "Fine.");
        }

        if(message == "PACKAGE_ADMIT") {
            DialogueManager.Say("Okay, I admit it. I did [INSERT NEFARIOUS ACTIVITY REGARDING PACKAGE].");
            DialogueManager.AddOption("OPEN", id, "Just as I suspected.");
            GameManager.JobFlags.SetStringFlag("PACKAGE_SAMMY_STATUS", "ADMITTED");
        }

        if(message == "PACKAGE_ACQUIT") {
            DialogueManager.Say("You'd better!");
            DialogueManager.AddOption("OPEN", id, "...");
            GameManager.JobFlags.SetStringFlag("PACKAGE_SAMMY_STATUS", "ACQUITTED");
        }

        #endregion

        #region INQUIRE_CHARACTER

        if(message == "INQUIRE_CHARACTER") {
            DialogueManager.Say("...");
            DialogueManager.AddOption("INQUIRE_DALE", id, "...Dale?");
            DialogueManager.AddOption("INQUIRE_HANK", id, "...Hank?");
            DialogueManager.AddOption("INQUIRE_ARLENE", id, "...Arlene?");
            DialogueManager.AddOption("OPEN", id, "< BACK");
            return;
        }

        if(message == "INQUIRE_HANK") {
            DialogueManager.Say("Hank's a total weirdo, but we don't talk, and that works for me.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if(message == "INQUIRE_ARLENE") {
            DialogueManager.Say("Arlene isn't a bad boss, but she's all wrong about Dale.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        #endregion

        #region DALE

        if(message == "INQUIRE_DALE") {
            GameManager.JobFlags.SetFlag("KNOWLEDGE_SAMMY_DALE", true);
            DialogueManager.Say("Waste-of-space, good-for-nothing, goody-two-shoes Dale. Is that who you mean?");
            DialogueManager.AddOption("OPEN", id, "...");
        }

        #endregion

    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
