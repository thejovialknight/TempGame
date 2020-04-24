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
            flagCollection.SetFlag("INTRODUCED", false); // DEBUG
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
    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
