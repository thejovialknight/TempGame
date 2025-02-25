﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arlene : NPC
{
    public Arlene()
    {
        id = "ARLENE";
        title = "Arlene";
        jobTitle = "General Manager";
    }

    /*
    public override void HandleDialogue(string message, params string[] args)
    {
        if(message == "OPEN")
        {
            if(DialogueManager.CheckArg(args, 0) == "NEVERMIND") 
            {
                DialogueManager.Say("Okay, then.");
            }
            else 
            {
                string introduction = "";
                if(!flagCollection.CheckFlag("INTRODUCED"))
                {
                    introduction += "Welcome to the job! ";
                    flagCollection.SetFlag("INTRODUCED", true);
                }

                DialogueManager.Say(introduction + "Do you have any questions for me?");
            }

            DialogueManager.AddOption("INQUIRE_WORK", id, "Any work I can do?");

            if(!GameManager.JobFlags.CheckFlag("KNOWLEDGE_PACKAGE"))
            {
                DialogueManager.AddOption("HUB_PACKAGE", id, "How are you doing?");
            }
            else if(GameManager.JobFlags.CheckFlag("KNOWLEDGE_PACKAGE"))
            {
                DialogueManager.AddOption("HUB_PACKAGE", id, "About the missing package...", "QUESTIONS");
            }

            if(GameManager.JobFlags.CheckFlag("KNOWLEDGE_SAMMY_DALE") && GameManager.GetQuest("DOWNSIZING").State == QuestState.Inactive)
            {
                DialogueManager.AddOption("INQUIRE_SAMMY_DALE", id, "What's the beef between Sammy and Dale?");
            }
            
            if(GameManager.GetQuest("DOWNSIZING").State == QuestState.InProgress) {
                DialogueManager.AddOption("DOWNSIZING_HUB", id, "About the downsizing.");
            }

            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "What do you think of...");
            DialogueManager.AddOption("CLOSE", id, "[X] Nope, see you later!");
            return;
        }

        #region HUB_PACKAGE

        if (message == "HUB_PACKAGE")
        {
            if (DialogueManager.CheckArg(args, 0) == "QUESTIONS")
            {
                DialogueManager.Say("Okay, shoot.");
            }
            else if (DialogueManager.CheckArg(args, 0) == "HUB")
            {
                DialogueManager.Say("Anything else?");
            }
            else
            {
                GameManager.JobFlags.SetFlag("KNOWLEDGE_PACKAGE", true);
                DialogueManager.Say("I've been better, to tell you the truth. I'm dealing with a missing package that's apparently pretty valuable.");
            }

            if(GameManager.JobFlags.CheckStringFlag("QUEST_PACKAGE") == null) {
                DialogueManager.AddOption("INQUIRE_PACKAGE_HELP", id, "Anything I can do?");
            }

            DialogueManager.AddOption("INQUIRE_PACKAGE_CONTENT", id, "What's in the box?");
            DialogueManager.AddOption("OPEN", id, "< BACK");
            return;
        }

        if (message == "INQUIRE_PACKAGE_CONTENT")
        {
            DialogueManager.Say("I'm not really allowed to say, but I can tell you it's a pain in my ass!");
            DialogueManager.AddOption("HUB_PACKAGE", id, "Got it.", "HUB");
            DialogueManager.AddOption("INQUIRE_PACKAGE_CONTENT_2", id, "What's in the box?? What's in the box!?");
            return;
        }

        if (message == "INQUIRE_PACKAGE_CONTENT_2")
        {
            flagCollection.SetFlag("PLAYER_IS_ODD", true);
            DialogueManager.Say("Hey, are you alright?");
            DialogueManager.AddOption("WHATS_BOX_YES", id, "Yes, I'm perfectly fine, why do you ask?");
            DialogueManager.AddOption("WHATS_BOX_NO", id, "No, not really.");
            return;
        }

        if (message == "WHATS_BOX_YES")
        {
            DialogueManager.Say("Oh, uh, nothing. You just seemed a little... nevermind.");
            DialogueManager.AddOption("HUB_PACKAGE", id, "Okay.", "HUB");
            return;
        }

        if (message == "WHATS_BOX_NO")
        {
            DialogueManager.Say("You know, you're a bit of a weird dude.");
            DialogueManager.AddOption("HUB_PACKAGE", id, "Yes.", "HUB");
            return;
        }

        if (message == "INQUIRE_PACKAGE_HELP")
        {
            DialogueManager.Say("If you could try gathering some information from the others that would probably help me get to the bottom of this.");
            DialogueManager.AddOption("ACCEPT_PACKAGE_HELP", id, "Yes, ma'am.");
            DialogueManager.AddOption("REJECT_PACKAGE_HELP", id, "Not my problem, sorry.");
            DialogueManager.AddOption("HUB_PACKAGE", id, "< I have some more questions first.", "QUESTIONS");
            return;
        }

        if (message == "ACCEPT_PACKAGE_HELP")
        {
            GameManager.JobFlags.SetStringFlag("QUEST_PACKAGE", "IN_PROGRESS");
            DialogueManager.Say("Thanks a lot!");
            DialogueManager.AddOption("HUB_PACKAGE", id, "No problem.", "HUB");
            return;
        }

        if (message == "REJECT_PACKAGE_HELP")
        {
            GameManager.JobFlags.SetStringFlag("QUEST_PACKAGE", "REJECTED");
            DialogueManager.Say("No need to be a dick, I wasn't asking for your help.");
            DialogueManager.AddOption("OPEN", id, "...");
            return;
        }

        #endregion

        #region INQUIRE_WORK

        if (message == "INQUIRE_WORK")
        {
            DialogueManager.Say("I'm sure your fellow employees have a lot on their plate at the moment, you should go ask one of them.");
            DialogueManager.AddOption("INQUIRE_WORK2", id, "...");
            return;
        }

        if (message == "INQUIRE_WORK2")
        {
            DialogueManager.Say("I think Sammy mentioned she was falling a little behind on sorting mail today, maybe you should try your hand at that!");
            DialogueManager.AddOption("OPEN", id, "...");
            return;
        }

        #endregion

        #region DOWNSIZING

        if(message == "DOWNSIZING_HUB") {
            if(DialogueManager.CheckArg(args, 0) != null) {
                DialogueManager.Say(DialogueManager.CheckArg(args, 0));
            }
            else {
                DialogueManager.Say("Sure thing.");
            }

            ListDownsizingQuestions();

            if(GameManager.GetQuest("DOWNSIZING").State == QuestState.Inactive) {
                DialogueManager.AddOption("DOWNSIZING_ACCEPT", id, "I can do that.");
                DialogueManager.AddOption("DOWNSIZING_REJECT", id, "I won't do it.");
            }
            else {
                if(GameManager.Day == 3 && GameManager.TimeChunk == 2) {
                    DialogueManager.AddOption("DOWNSIZING_RECOMMEND_HUB", id, "I'm ready to make a recommendation.");
                }
                DialogueManager.AddOption("OPEN", id, "< BACK");
            }

            return;
        }

        if(message == "INQUIRE_SAMMY_DALE") {
            DialogueManager.Say("God, those two are a pain in my ass. I don't know what it is, but I'm sure Sammy started it.");
            DialogueManager.AddOption("INQUIRE_SAMMY_DALE2", id, "...");
            return;
        }

        if(message == "INQUIRE_SAMMY_DALE2") {
            DialogueManager.Say("Speaking of that, I have a small task for you, if you're up for it.");
            DialogueManager.AddOption("DOWNSIZING_LISTEN", id, "I'm listening.");
            DialogueManager.AddOption("DOWNSIZING_REJECT", id, "Not a chance.");
            return;
        }

        if(message == "DOWNSIZING_LISTEN") {
            DialogueManager.Say("Don't tell anyone, but corporate has been on my ass lately about downsizing the office.");
            DialogueManager.AddOption("DOWNSIZING_LISTEN2", id, "...");
            return;
        }

        if(message == "DOWNSIZING_LISTEN2") {
            DialogueManager.Say("They want me to fire someone by the end of the week.");
            DialogueManager.AddOption("INQUIRE_TEMP_HIRE", id, "Why did you hire me, then?");
            DialogueManager.AddOption("DOWNSIZING_HUB", id, "...", "Could you gather some information about your fellow employees and report them back to me at the end of your three day period here?");
            return;
        }

        if(message == "INQUIRE_TEMP_HIRE") {
            DialogueManager.Say("Oh. Well, nevermind that.");
            DialogueManager.AddOption("DOWNSIZING_HUB", id, "Alright.", "Could you gather some information about your fellow employees and report them back to me at the end of your three day period here?");
            return;
        }

        if(message == "DOWNSIZING_ACCEPT") {
            GameManager.StartQuest("DOWNSIZING");

            DialogueManager.Say("Fantastic!");
            DialogueManager.AddOption("DOWNSIZING_HUB", id, "...", "Any other questions?");
            return;
        }

        if(message == "DOWNSIZING_REJECT") {
            GameManager.RejectQuest("DOWNSIZING");

            DialogueManager.Say("I can't say I blame you. That's okay, I'll find someone else.");
            DialogueManager.AddOption("OPEN", id, "...");
            return;
        }

        if (message == "INQUIRE_SPYING")
        {
            DialogueManager.Say("Well, not so much spying as much as...");
            DialogueManager.AddOption("INQUIRE_SPYING2", id, "...");
            return;
        }

        if (message == "INQUIRE_SPYING2")
        {
            DialogueManager.Say("I mean, yes, it is. I'm asking you to spy, yes.");
            DialogueManager.AddOption("DOWNSIZING_HUB", id, "...");
            return;
        }

        #endregion
        
        #region RECOMMENDATION HUB

        if(message == "DOWNSIZING_RECOMMEND_HUB") {
            if(DialogueManager.CheckArg(args, 0) != null) {
                DialogueManager.Say(DialogueManager.CheckArg(args, 0));
            }
            else {
                DialogueManager.Say("What do you have for me?");
            }

            DialogueManager.AddOption("FIRE_DALE", id, "It's time to bail on Dale.");
            DialogueManager.AddOption("FIRE_SAMMY", id, "Sammy's the one that's hammy.");
            DialogueManager.AddOption("FIRE_HANK", id, "You've gotta tank Hank.");
            DialogueManager.AddOption("FIRE_ARLENE", id, "Unfortunately, it's you who's overdue.");
            DialogueManager.AddOption("DOWNSIZING_HUB", id, "< BACK");
        }

        #endregion

        #region FIRING DALE

        if(message == "FIRE_DALE") {
            if(GameManager.GetQuest("DOWNSIZING").Flags.CheckIntFlag("DALE_EVIDENCE") >= 2) {
                DialogueManager.Say("Well, that's all I need. I guess Dale's gotta go. What a shame, he was a hard worker.");
                GameManager.GetQuest("DOWNSIZING").Flags.SetStringFlag("RECOMMENDATION", "DALE");
                GameManager.GetQuest("DOWNSIZING").State = QuestState.Complete;
                DialogueManager.AddOption("OPEN", id, "...");
            } 
            else {
                if(DialogueManager.CheckArg(args, 0) != null) {
                    DialogueManager.Say(DialogueManager.CheckArg(args, 0));
                }
                else {
                    DialogueManager.Say("Oh, really?");
                }

                if(GameManager.GetQuest("DOWNSIZING").Flags.CheckFlag("DALE_EVIDENCE_LAZY")) {
                    DialogueManager.AddOption("DALE_EVIDENCE_LAZY", id, "He has way too much time on his hands.");
                }

                if(GameManager.GetQuest("DOWNSIZING").Flags.CheckFlag("DALE_EVIDENCE_THIEF")) {
                    DialogueManager.AddOption("DALE_EVIDENCE_THIEF", id, "He stole the missing package.");
                }

                DialogueManager.AddOption("DOWNSIZING_RECOMMEND_HUB", id, "< Never mind.");
            }
        }

        if(message == "DALE_EVIDENCE_LAZY") {
            DialogueManager.Say("Lazy, really? If you say so.");
            DialogueManager.AddOption("FIRE_DALE", id, "...", "What else?");

            GameManager.GetQuest("DOWNSIZING").Flags.ModifyIntFlag("DALE_EVIDENCE", 1);
            return;
        }

        if(message == "DALE_EVIDENCE_THIEF") {
            DialogueManager.Say("He did? The motherfucker or...");
            DialogueManager.AddOption("FIRE_DALE", id, "...", "What else?");

            GameManager.GetQuest("DOWNSIZING").Flags.ModifyIntFlag("DALE_EVIDENCE", 1);
            return;
        }

        #endregion

        #region FIRING SAMMY

        if(message == "FIRE_SAMMY") {
            if(GameManager.GetQuest("DOWNSIZING").Flags.CheckIntFlag("SAMMY_EVIDENCE") >= 2) {
                DialogueManager.Say("Well, that's all I need. I can't say I'm surprised, Sammy didn't always play nice.");
                GameManager.GetQuest("DOWNSIZING").Flags.SetStringFlag("RECOMMENDATION", "SAMMY");
                GameManager.GetQuest("DOWNSIZING").State = QuestState.Complete;
                DialogueManager.AddOption("OPEN", id, "...");
            } 
            else {
                if(DialogueManager.CheckArg(args, 0) != null) {
                    DialogueManager.Say(DialogueManager.CheckArg(args, 0));
                }
                else {
                    DialogueManager.Say("Oh, really?");
                }

                if(GameManager.GetQuest("DOWNSIZING").Flags.CheckFlag("SAMMY_EVIDENCE_PACKAGE")) {
                    DialogueManager.AddOption("SAMMY_EVIDENCE_PACKAGE", id, "She hid the missing package.");
                }

                if(GameManager.GetQuest("DOWNSIZING").Flags.CheckFlag("SAMMY_EVIDENCE_SABOTAGE")) {
                    DialogueManager.AddOption("SAMMY_EVIDENCE_SABOTAGE", id, "Sammy tried to frame Dale.");
                }

                DialogueManager.AddOption("DOWNSIZING_RECOMMEND_HUB", id, "< Never mind.");
            }
        }

        if(message == "SAMMY_EVIDENCE_PACKAGE") {
            DialogueManager.Say("Hid the package? Damn!!");
            DialogueManager.AddOption("FIRE_SAMMY", id, "...", "What else?");

            GameManager.GetQuest("DOWNSIZING").Flags.ModifyIntFlag("SAMMY_EVIDENCE", 1);
            return;
        }

        if(message == "SAMMY_EVIDENCE_SABOTAGE") {
            DialogueManager.Say("That's nasty...");
            DialogueManager.AddOption("FIRE_SAMMY", id, "...", "What else?");

            GameManager.GetQuest("DOWNSIZING").Flags.ModifyIntFlag("SAMMY_EVIDENCE", 1);
            return;
        }

        #endregion

        #region FIRING HANK

        if(message == "FIRE_HANK") {
            if(GameManager.GetQuest("DOWNSIZING").Flags.CheckIntFlag("HANK_EVIDENCE") >= 2) {
                DialogueManager.Say("Well, that's all I need. I knew he was a weirdo, but I didn't know all that.");
                GameManager.GetQuest("DOWNSIZING").Flags.SetStringFlag("RECOMMENDATION", "HANK");
                GameManager.GetQuest("DOWNSIZING").State = QuestState.Complete;
                DialogueManager.AddOption("OPEN", id, "...");
            } 
            else {
                if(DialogueManager.CheckArg(args, 0) != null) {
                    DialogueManager.Say(DialogueManager.CheckArg(args, 0));
                }
                else {
                    DialogueManager.Say("Oh, really?");
                }

                if(GameManager.GetQuest("DOWNSIZING").Flags.CheckFlag("HANK_EVIDENCE_LABELS")) {
                    DialogueManager.AddOption("HANK_EVIDENCE_LABELS", id, "He mixed up the labels.");
                }

                if(GameManager.GetQuest("DOWNSIZING").Flags.CheckFlag("HANK_DRUG_DEALER")) {
                    DialogueManager.AddOption("HANK_DRUG_DEALER", id, "Hank is a drug dealer.");
                }

                DialogueManager.AddOption("DOWNSIZING_RECOMMEND_HUB", id, "< Never mind.");
            }
        }

        if(message == "HANK_EVIDENCE_LABELS") {
            DialogueManager.Say("Hid the package? Damn!!");
            DialogueManager.AddOption("FIRE_HANK", id, "...", "What else?");

            GameManager.GetQuest("DOWNSIZING").Flags.ModifyIntFlag("HANK_EVIDENCE", 1);
            return;
        }

        if(message == "HANK_DRUG_DEALER") {
            DialogueManager.Say("That's nasty...");
            DialogueManager.AddOption("FIRE_HANK", id, "...", "What else?");

            GameManager.GetQuest("DOWNSIZING").Flags.ModifyIntFlag("HANK_EVIDENCE", 1);
            return;
        }

        #endregion

        #region INQUIRE_CHARACTER

        if (message == "INQUIRE_CHARACTER")
        {
            DialogueManager.Say("...");
            DialogueManager.AddOption("INQUIRE_DALE", id, "...Dale?");
            DialogueManager.AddOption("INQUIRE_SAMMY", id, "...Sammy?");
            DialogueManager.AddOption("INQUIRE_HANK", id, "...Hank?");
            DialogueManager.AddOption("OPEN", id, "< BACK");
            return;
        }

        if (message == "INQUIRE_DALE")
        {
            DialogueManager.Say("Dale is a hard worker and isn't quite as dull as he seems. He's not great with customers, though.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if (message == "INQUIRE_SAMMY")
        {
            DialogueManager.Say("Sammy is a bit intense, but she does her job quickly and efficiently, so I can't complain.");
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        if (message == "INQUIRE_HANK")
        {
            if(flagCollection.CheckFlag("PLAYER_IS_ODD")) {
                DialogueManager.Say("Hank's a bit... odd. I'm sure you two woud get along nicely.");
            }
            else {
                DialogueManager.Say("Between you and me, Hank's a bit of a weirdo, but I suppose that's none of my business.");
            }
            DialogueManager.AddOption("INQUIRE_CHARACTER", id, "...");
            return;
        }

        #endregion

    }

    #region DIALOGUE METHODS

    void ListDownsizingQuestions() {
        DialogueManager.AddOption("INQUIRE_SPYING", id, "You want me to spy on my colleagues?");
    }

    */

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }

}
