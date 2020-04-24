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

    public override void OnProgressDay(int day) {
        if(day == 2) {
            transform.position = new Vector2(-200f, 0f);
        }
    }

    public override void OnProgressTime(int time) {
        
    }

    public override void HandleDialogue(string message, string[] args)
    {
        if (message == "OPEN")
        {
            if (!flagCollection.CheckFlag("INTRODUCED"))
            {
                flagCollection.SetFlag("INTRODUCED", true);
                DialogueManager.instance.Say("Hey, man, you ever tried tortoise?");
                DialogueManager.instance.AddOption("TORTOISE_WHAT", id, "I'm sorry, what was that?");
                DialogueManager.instance.AddOption("TORTOISE_INQUIRE_FOOD", id, "As food or as a friend?");
                DialogueManager.instance.AddOption("TORTOISE_NO", id, "I don't think so, no.");
                DialogueManager.instance.AddOption("TORTOISE_SURE", id, "Sure.");
                DialogueManager.instance.AddOption("CLOSE", id, "Okay, nevermind.");
            }
            else 
            {
                DialogueManager.instance.Say("What's the what?");

                DialogueManager.instance.AddOption("NO_WORK", id, "Got any work for me?");

                if(GameManager.instance.currentJob.flagCollection.CheckFlag("KNOWLEDGE_PACKAGE")) {
                    DialogueManager.instance.AddOption("MISSING_PACKAGE", id, "I have some questions about the missing package.");
                }
                else {
                    DialogueManager.instance.AddOption("MISSING_PACKAGE", id, "What's up with you?");
                }

                DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "What do you think of...");
                DialogueManager.instance.AddOption("CLOSE", id, "[X] Oh, nothing much.");
            }
            return;
        }

        #region TORTOISE

        if (message == "TORTOISE_WHAT")
        {
            DialogueManager.instance.Say("You know, tortoise.");
            DialogueManager.instance.AddOption("TORTOISE_SURE", id, "Sure.");
        }

        if (message == "TORTOISE_INQUIRE_FOOD")
        {
            DialogueManager.instance.Say("Either or, I'd say.");
            DialogueManager.instance.AddOption("TORTOISE_SURE", id, "Sure.");
            DialogueManager.instance.AddOption("TORTOISE_NO", id, "No, definitely not.");
            DialogueManager.instance.AddOption("TORTOISE_IDK", id, "I can't answer without more information");
        }

        if (message == "TORTOISE_SURE")
        {
            DialogueManager.instance.Say("You haven't tried tortoise. I can always tell when someone's tried tortoise, and you haven't.");
            DialogueManager.instance.AddOption("TORTOISE_NO", id, "Okay, you got me.");
        }

        if (message == "TORTOISE_NO")
        {
            DialogueManager.instance.Say("That's okay, I'm not surprised.");
            DialogueManager.instance.AddOption("CLOSE", id, "See you later!");
        }

        if (message == "TORTOISE_IDK")
        {
            DialogueManager.instance.Say("That's understandable. I'll let it go.");
            DialogueManager.instance.AddOption("CLOSE", id, "Okay.");
        }

        #endregion

        if(message == "INQUIRE_WORK") {
            DialogueManager.instance.Say("Nothing going at the moment, I just got back from my morning delivery.");
            DialogueManager.instance.AddOption("OPEN", id, "...");
            return;
        }

        #region INQUIRE_CHARACTER

        if(message == "INQUIRE_CHARACTER") {
            DialogueManager.instance.Say("...");
            DialogueManager.instance.AddOption("INQUIRE_DALE", id, "...Dale?");
            DialogueManager.instance.AddOption("INQUIRE_SAMMY", id, "...Sammy?");
            DialogueManager.instance.AddOption("INQUIRE_ARLENE", id, "...Arlene?");
            DialogueManager.instance.AddOption("OPEN", id, "< BACK");
            return;
        }

        if(message == "INQUIRE_DALE") {
            DialogueManager.instance.Say("Dale's a bit quiet, but he's alright.");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if(message == "INQUIRE_SAMMY") {
            DialogueManager.instance.Say("Sammy scares me a bit, but I've been through far worse.");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if(message == "INQUIRE_ARLENE") {
            DialogueManager.instance.Say("Arlene's great. Tough, but fair, and lets me do my job.");
            DialogueManager.instance.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        #endregion

        #region MISSING_PACKAGE

        if(message == "MISSING_PACKAGE") {
            if(!GameManager.instance.currentJob.flagCollection.CheckFlag("KNOWLEDGE_PACKAGE")) {
                GameManager.instance.currentJob.flagCollection.SetFlag("KNOWLEDGE_PACKAGE", true);
                DialogueManager.instance.Say("Oh, we're all having a rough morning what with the missing package.");
                DialogueManager.instance.AddOption("INQUIRE_PACKAGE", id, "What missing package?");
            }
            else {
                DialogueManager.instance.Say("I'm all ears.");
            }

            if(GameManager.instance.currentJob.flagCollection.CheckStringFlag("PACKAGE_HANK_STATUS") != "ADMITTED") {
                DialogueManager.instance.AddOption("INQUIRE_PACKAGE_BLAME", id, "Was it you who lost it?");
            }
            DialogueManager.instance.AddOption("OPEN", id, "< BACK");
            return;
        }

        if(message == "INQUIRE_PACKAGE") {
            DialogueManager.instance.Say("It's just some package.");
            DialogueManager.instance.AddOption("MISSING_PACKAGE", id, "...");
        }

        if(message == "INQUIRE_PACKAGE_BLAME") {
            DialogueManager.instance.Say("Wasn't me, and you can take that to the bank!");
            DialogueManager.instance.AddOption("PACKAGE_ACCUSE", id, "You're lying!");
            DialogueManager.instance.AddOption("PACKAGE_ACQUIT", id, "Okay, I believe you.");
            DialogueManager.instance.AddOption("MISSING_PACKAGE", id, "< I have more questions.");
            GameManager.instance.currentJob.flagCollection.SetStringFlag("PACKAGE_HANK_STATUS", "DENIED");
        }

        if(message == "PACKAGE_ACCUSE") {
            DialogueManager.instance.Say("Well, where's your proof?");
            DialogueManager.instance.AddOption("PACKAGE_NO_PROOF", id, "That's all I have...");
        }

        if(message == "PACKAGE_NO_PROOF") {
            DialogueManager.instance.Say("Sorry, bud. You've got the wrong guy.");
            DialogueManager.instance.AddOption("MISSING_PACKAGE", id, "Fine.");
        }

        if(message == "PACKAGE_ADMIT") {
            DialogueManager.instance.Say("Okay, I admit it. I did [INSERT NEFARIOUS ACTIVITY REGARDING PACKAGE].");
            DialogueManager.instance.AddOption("OPEN", id, "Just as I suspected.");
            GameManager.instance.currentJob.flagCollection.SetStringFlag("PACKAGE_HANK_STATUS", "ADMITTED");
        }

        if(message == "PACKAGE_ACQUIT") {
            DialogueManager.instance.Say("I knew you'd understand. I'm not a thief.");
            DialogueManager.instance.AddOption("OPEN", id, "...");
            GameManager.instance.currentJob.flagCollection.SetStringFlag("PACKAGE_HANK_STATUS", "ACQUITTED");
        }

        #endregion

    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
