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
            DialogueManager.instance.Say("Hey, man. What's the what?");
            DialogueManager.instance.AddOption("CLOSE", id, "Nothing much.");
            return;
        }
    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
