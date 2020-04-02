using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hank : NPC
{
    public override void OnReceiveMessage(string message)
    {
        #region Dialogue

        if(CheckMessage(message, "OPEN"))
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

        if(CheckMessage(message, "FRIENDS"))
        {
            SetFlag("ANGRY", false);
            SetFlag("FRIENDS", true);

            Say("It's okay, bro.");
            AddOption("...", "END");
        }

        if(CheckMessage(message, "ANGRY"))
        {
            SetFlag("ANGRY", true);
            SetFlag("FRIENDS", false);

            Say("Hey, fuck you! I'm so angry, I will walk forward!");
            MessageEventManager.BroadcastCutscene("HANK_WALK_FORWARD");
            AddOption("...", "END");
        }

        if(CheckMessage(message, "END"))
        {
            CloseDialogue();
        }

        #endregion

        #region Cutscenes

        if(CheckCutsceneMessage(message, "HANK_WALK_FORWARD"))
        {
            transform.Translate(new Vector3(2f, 0f, 0f));
        }

        #endregion
    }
}
