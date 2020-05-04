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

    public override void HandleDialogue(string message, string[] args)
    {
        if(message == "OPEN")
        {
            DialogueManager.Say("Hey.");

            DialogueManager.AddOption("INQUIRE_MOOD", id, "How are you?");

            SammyRevealOption("I have something to tell you about Sammy...");

            if(GameManager.JobFlags.CheckFlag("KNOWLEDGE_SAMMY_DALE")) {
                DialogueManager.AddOption("INQUIRE_SAMMY_DALE", id, "What's with you and Sammy?");
            }

            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "What do you think of...");
            DialogueManager.AddOption("CLOSE", id, "Bye!");
            return;
        }

        if(message == "INQUIRE_SAMMY_DALE") {
            DialogueManager.Say("Ask her, I'm just trying to do my job.");
            SammyRevealOption("I have, and she's planning something...");
            DialogueManager.AddOption("OPEN", id, "Fair enough.");
        }

        #region REVEAL_SAMMY_PLOT

        if(message == "REVEAL_SAMMY_PLOT") {
            DialogueManager.Say("Huh?");
            DialogueManager.AddOption("SAMMY_PLOT_TRUTH", id, "She's trying to get you fired.");
            DialogueManager.AddOption("SAMMY_PLOT_LIE_PARTY", id, "[LIE] She's throwing you a birthday party!");
            DialogueManager.AddOption("OPEN", id, "< Nevermind...");
        }

        if(message == "SAMMY_PLOT_TRUTH") {
            DialogueManager.Say("Oh, thanks for telling me.");
            DialogueManager.AddOption("OPEN", id, "No problem!");
            DialogueManager.AddOption("SAMMY_PLOT_LIE_PARTY", id, "I will tell her her plan is foiled.");
            DialogueManager.AddOption("SAMMY_PLOT_LIE_PARTY", id, "I will be a double agent.");
            flagCollection.SetIntFlag("DISPOSITION", 5);
            GameManager.GetQuest("DALE_SAMMY").Flags.SetStringFlag("DALE_KNOWLEDGE", "TRUTH");
        }

        if(message == "SAMMY_PLOT_LIE_PARTY") {
            DialogueManager.Say("My birthday isn't until April.");
            DialogueManager.AddOption("OPEN", id, "< Nevermind...");
        }

        #endregion

        #region SAMMY_PLOT_HUB

        if(message == "SAMMY_PLOT_HUB") {
            DialogueManager.Say("Yeah?");
            DialogueManager.AddOption("OPEN", id, "< Nevermind...");
            DialogueManager.AddOption("OPEN", id, "< Nevermind...");
            DialogueManager.AddOption("OPEN", id, "< Nevermind...");
        }
        
        #endregion

        #region INQUIRE_CHARACTER

        if(message == "INQUIRE_CHARACTER") {
            DialogueManager.Say("...");
            DialogueManager.AddOption("INQUIRE_SAMMY", id, "...Sammy?");
            DialogueManager.AddOption("INQUIRE_HANK", id, "...Hank?");
            DialogueManager.AddOption("INQUIRE_ARLENE", id, "...Arlene?");
            DialogueManager.AddOption("OPEN", id, "< BACK");
            return;
        }

        if(message == "INQUIRE_SAMMY") {
            DialogueManager.Say("Sammy doesn't like me very much. I suppose I don't like her very much either.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            SammyRevealOption("Speaking of Sammy, I should tell you something...");
            GameManager.JobFlags.SetFlag("KNOWLEDGE_SAMMY_DALE", true);
            return;
        }

        if(message == "INQUIRE_HANK") {
            DialogueManager.Say("Hank's okay, I guess.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if(message == "INQUIRE_ARLENE") {
            DialogueManager.Say("Arlene's good.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        #endregion

    }

    #region Dialogue Methods

    void SammyRevealOption(string text) {
        if(GameManager.JobFlags.CheckFlag("KNOWLEDGE_SAMMY_PLOT")) {
            DialogueManager.AddOption("REVEAL_SAMMY_PLOT", id, text);
        }
    }

    #endregion

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
