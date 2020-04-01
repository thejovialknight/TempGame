using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hank : NPC
{
    public override void OnReceiveMessage(string message)
    {
        if(message == id + "_OPEN")
        {
            if(flagCollection.CheckFlag("ANGRY"))
            {
                DialogueManager.instance.Say("I don't really wanna talk to you, man.");
                DialogueManager.instance.AddOption(new DialogueOption(id + "_ANGRY", "And I don't want to talk to you either!"));
                DialogueManager.instance.AddOption(new DialogueOption(id + "_FRIENDS", "Sorry, I really didn't mean it."));
            }
            else if(flagCollection.CheckFlag("FRIENDS"))
            {
                DialogueManager.instance.Say("Hey, buddy. Good to see you again.");
                DialogueManager.instance.AddOption(new DialogueOption(id + "_ANGRY", "Hey, man. I'm not your buddy"));
                DialogueManager.instance.AddOption(new DialogueOption(id + "_FRIENDS", "Hey, man! Sorry about your troubles!"));
            }
            else
            {
                DialogueManager.instance.Say("Hey, man. I'm a little bummed today.");
                DialogueManager.instance.AddOption(new DialogueOption(id + "_ANGRY", "You're pathetic."));
                DialogueManager.instance.AddOption(new DialogueOption(id + "_FRIENDS", "Sorry, bro."));
            }
        }

        if(message == id + "_FRIENDS")
        {
            flagCollection.SetFlag("ANGRY", false);
            flagCollection.SetFlag("FRIENDS", true);
            DialogueManager.instance.Say("It's okay, bro.");
            DialogueManager.instance.AddOption(new DialogueOption(id + "_END", "..."));
        }

        if(message == id + "_ANGRY")
        {
            flagCollection.SetFlag("ANGRY", true);
            flagCollection.SetFlag("FRIENDS", false);
            DialogueManager.instance.Say("Hey, fuck you!");
            DialogueManager.instance.AddOption(new DialogueOption(id + "_END", "..."));
        }

        if(message == id + "_END")
        {
            DialogueManager.instance.Close();
        }
    }
}
