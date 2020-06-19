using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hank : NPC
{
    public GameObject deliveryObject;

    public Hank()
    {
        id = "HANK";
        title = "Hank";
        jobTitle = "Delivery";
    }

/*
    void Start()
    {
        deliveryObject.SetActive(true);
        MessageEventManager.MinigameStart("DELIVERY");
    }
    */

    public override void OnProgressDay(int day) {
        if(day == 2) {
            transform.position = new Vector2(-200f, 0f);
        }
    }

    public override void OnProgressTime(int time) {
        
    }

    /*

    public override void HandleDialogue(string message, string[] args)
    {
        if (message == "OPEN")
        {
            if (!flagCollection.CheckFlag("INTRODUCED"))
            {
                flagCollection.SetFlag("INTRODUCED", true);
                DialogueManager.Say("Hey, man, you ever tried tortoise?");
                DialogueManager.AddOption("TORTOISE_WHAT", id, "I'm sorry, what was that?");
                DialogueManager.AddOption("TORTOISE_INQUIRE_FOOD", id, "As food or as a friend?");
                DialogueManager.AddOption("TORTOISE_NO", id, "I don't think so, no.");
                DialogueManager.AddOption("TORTOISE_SURE", id, "Sure.");
                DialogueManager.AddOption("CLOSE", id, "Okay, nevermind.");
            }
            else 
            {
                DialogueManager.Say("What's the what?");

                DialogueManager.AddOption("MINIGAME", id, "Got any work for me?"); // should be NO_WORK if day 1

                if(GameManager.JobFlags.CheckFlag("KNOWLEDGE_PACKAGE")) {
                    DialogueManager.AddOption("MISSING_PACKAGE", id, "I have some questions about the missing package.");
                }
                else {
                    DialogueManager.AddOption("MISSING_PACKAGE", id, "What's up with you?");
                }

                DialogueManager.AddOption("INQUIRE_CHARACTER", id, "What do you think of...");
                DialogueManager.AddOption("CLOSE", id, "[X] Oh, nothing much.");
            }
            return;
        }

        if (message == "MINIGAME")
        {
            DialogueManager.Close();
            deliveryObject.SetActive(true);
            MessageEventManager.MinigameStart("DELIVERY");
            return;
        }

        #region TORTOISE

        if (message == "TORTOISE_WHAT")
        {
            DialogueManager.Say("You know, tortoise.");
            DialogueManager.AddOption("TORTOISE_SURE", id, "Sure.");
        }

        if (message == "TORTOISE_INQUIRE_FOOD")
        {
            DialogueManager.Say("Either or, I'd say.");
            DialogueManager.AddOption("TORTOISE_SURE", id, "Sure.");
            DialogueManager.AddOption("TORTOISE_NO", id, "No, definitely not.");
            DialogueManager.AddOption("TORTOISE_IDK", id, "I can't answer without more information");
        }

        if (message == "TORTOISE_SURE")
        {
            DialogueManager.Say("You haven't tried tortoise. I can always tell when someone's tried tortoise, and you haven't.");
            DialogueManager.AddOption("TORTOISE_NO", id, "Okay, you got me.");
        }

        if (message == "TORTOISE_NO")
        {
            DialogueManager.Say("That's okay, I'm not surprised.");
            DialogueManager.AddOption("CLOSE", id, "See you later!");
        }

        if (message == "TORTOISE_IDK")
        {
            DialogueManager.Say("That's understandable. I'll let it go.");
            DialogueManager.AddOption("CLOSE", id, "Okay.");
        }

        #endregion

        if(message == "INQUIRE_WORK") {
            DialogueManager.Say("Nothing going at the moment, I just got back from my morning delivery.");
            DialogueManager.AddOption("OPEN", id, "...");
            return;
        }

        #region INQUIRE_CHARACTER

        if(message == "INQUIRE_CHARACTER") {
            DialogueManager.Say("...");
            DialogueManager.AddOption("INQUIRE_DALE", id, "...Dale?");
            DialogueManager.AddOption("INQUIRE_SAMMY", id, "...Sammy?");
            DialogueManager.AddOption("INQUIRE_ARLENE", id, "...Arlene?");
            DialogueManager.AddOption("OPEN", id, "< BACK");
            return;
        }

        if(message == "INQUIRE_DALE") {
            DialogueManager.Say("Dale's a bit quiet, but he's alright.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if(message == "INQUIRE_SAMMY") {
            DialogueManager.Say("Sammy scares me a bit, but I've been through far worse.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if(message == "INQUIRE_ARLENE") {
            DialogueManager.Say("Arlene's great. Tough, but fair, and lets me do my job.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        #endregion

        #region MISSING_PACKAGE

        if(message == "MISSING_PACKAGE") {
            if(!GameManager.JobFlags.CheckFlag("KNOWLEDGE_PACKAGE")) {
                GameManager.JobFlags.SetFlag("KNOWLEDGE_PACKAGE", true);
                DialogueManager.Say("Oh, we're all having a rough morning what with the missing package.");
                DialogueManager.AddOption("INQUIRE_PACKAGE", id, "What missing package?");
            }
            else {
                DialogueManager.Say("I'm all ears.");
            }

            if(GameManager.JobFlags.CheckStringFlag("PACKAGE_HANK_STATUS") != "ADMITTED") {
                DialogueManager.AddOption("INQUIRE_PACKAGE_BLAME", id, "Was it you who lost it?");
            }
            DialogueManager.AddOption("OPEN", id, "< BACK");
            return;
        }

        if(message == "INQUIRE_PACKAGE") {
            DialogueManager.Say("It's just some package.");
            DialogueManager.AddOption("MISSING_PACKAGE", id, "...");
        }

        if(message == "INQUIRE_PACKAGE_BLAME") {
            DialogueManager.Say("Wasn't me, and you can take that to the bank!");
            DialogueManager.AddOption("PACKAGE_ACCUSE", id, "You're lying!");
            DialogueManager.AddOption("PACKAGE_ACQUIT", id, "Okay, I believe you.");
            DialogueManager.AddOption("MISSING_PACKAGE", id, "< I have more questions.");
            GameManager.JobFlags.SetStringFlag("PACKAGE_HANK_STATUS", "DENIED");
        }

        if(message == "PACKAGE_ACCUSE") {
            DialogueManager.Say("Well, where's your proof?");
            DialogueManager.AddOption("PACKAGE_NO_PROOF", id, "That's all I have...");
        }

        if(message == "PACKAGE_NO_PROOF") {
            DialogueManager.Say("Sorry, bud. You've got the wrong guy.");
            DialogueManager.AddOption("MISSING_PACKAGE", id, "Fine.");
        }

        if(message == "PACKAGE_ADMIT") {
            DialogueManager.Say("Okay, I admit it. I did [INSERT NEFARIOUS ACTIVITY REGARDING PACKAGE].");
            DialogueManager.AddOption("OPEN", id, "Just as I suspected.");
            GameManager.JobFlags.SetStringFlag("PACKAGE_HANK_STATUS", "ADMITTED");
        }

        if(message == "PACKAGE_ACQUIT") {
            DialogueManager.Say("I knew you'd understand. I'm not a thief.");
            DialogueManager.AddOption("OPEN", id, "...");
            GameManager.JobFlags.SetStringFlag("PACKAGE_HANK_STATUS", "ACQUITTED");
        }

        #endregion

    }

    */

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
