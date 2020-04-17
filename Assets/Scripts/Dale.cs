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

    public override void OnDialogue(string id, string message)
    {
        base.OnDialogue(id, message);

        if(id != this.id) {
            return;
        }

        if(message == "OPEN")
        {
            if(GameManager.instance.activeItem.id == "DALE_WEDDING_RING") {
                Say("My god, you've found it. You've found my wedding ring!");
                AddOption("Here you go, buddy!", "GIVE_RING");
                AddOption("The ring is mine, Dale.", "KEEP_RING");
            }
            else {
                Say("Hey, man, I don't wanna talk. I lost my wedding ring...");
                AddOption("...", "END");
            }
        }

        if(message == "GIVE_RING") {
            GameManager.instance.RemoveItem("DALE_WEDDING_RING");
            Say("I am your friend, forever...");
            AddOption("...", "END");
        }

        if(message == "KEEP_RING") {
            Say("NOOOOOOOOOOOOOOO!!");
            AddOption("...", "END");
        }

        if(message == "END") {
            CloseDialogue();
        }
    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
