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

    public override void OnDialogue(string id, string message)
    {
        base.OnDialogue(id, message);

        if(id != this.id) {
            return;
        }
        
        if(message == "OPEN")
        {
            if(CheckFlag("ANGRY"))
            {
                Say("I don't really wanna talk to you, man.");
                AddOption("And I don't want to talk to you either!", "ANGRY");
                AddOption("Sorry, I really didn't mean it.", "FRIENDS");
            }
            else if(CheckFlag("FRIENDS"))
            {
                Say("Hey, buddy. Good to see you again.");
                AddOption("Hey, man. I'm not your buddy", "ANGRY");
                AddOption("Hey, man! Sorry about your troubles!", "FRIENDS");
            }
            else
            {
                Say("Hey, man. I'm a little bummed today.");
                AddOption("You're pathetic.", "ANGRY");
                AddOption("Sorry, bro.", "FRIENDS");
            }
        }

        if(message == "FRIENDS")
        {
            Debug.Log("FRIEND");
            SetFlag("ANGRY", false);
            SetFlag("FRIENDS", true);

            Say("It's okay, bro.");
            AddOption("...", "END");
        }

        if(message == "ANGRY")
        {
            Debug.Log("ANGER");
            SetFlag("ANGRY", true);
            SetFlag("FRIENDS", false);

            Say("Hey, fuck you! I'm so angry, I will walk forward!");
            AddOption("...", "WALK");
        }

        if(message == "WALK")
        {
            CloseDialogue();
            MessageEventManager.Cutscene("HANK_WALK_FORWARD");
        }

        if(message == "WALKED")
        {
            Say("See? I have walked!");
            AddOption("...", "END");
        }

        if(message == "END")
        {
            CloseDialogue();
        }
    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);

        if(message == "HANK_WALK_FORWARD") {
            StartCoroutine(Walk(3f));
        }
    }

    IEnumerator Walk(float length) {
        MessageEventManager.Pause(true, true, this);
        for (float count = 0f; count <= length; count += Time.deltaTime) 
        {
            transform.Translate(new Vector3(0f, Time.deltaTime, 0f));
            yield return null;
        }
        MessageEventManager.Resume();
        MessageEventManager.Dialogue(id, "WALKED");
    }
}
