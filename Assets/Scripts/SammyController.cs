using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SammyController : MonoBehaviour, IInteractable
{
    DialogueManager dialogueManager;

    public void InteractWith(Transform interactor) {
        MessageEventManager.RaiseOnReceiveMessage("SAMMY_OPEN");
    }

    public string GetInteractName()
    {
        return "Sammy (Warehouse)";
    }

    public string GetInteractInfo() {
        return "Press E to talk";
    }

    void OnEnable()
    {
        MessageEventManager.OnReceiveMessageEvent += OnReceiveMessage;
    }

    void OnDisable()
    {
        MessageEventManager.OnReceiveMessageEvent -= OnReceiveMessage;
    }

    void OnReceiveMessage(string id)
    {
        switch(id)
        {
            case "SAMMY_OPEN" :
                dialogueManager.Say("Who are you lookin' at?");
                dialogueManager.AddOption(new DialogueOption("SAMMY_UH", "Uh..."));
                dialogueManager.AddOption(new DialogueOption("SAMMY_WHOAREYOU", "I don't know, who are you?"));
                dialogueManager.AddOption(new DialogueOption("SAMMY_HOWGOESIT", "How are you?"));
                break;
            case "SAMMY_UH" :
                dialogueManager.Say("Hey, I asked you a question!");
                dialogueManager.AddOption(new DialogueOption("SAMMY_UH2", "I..."));
                break;
            case "SAMMY_UH2" :
                dialogueManager.Say("Quit staring and get back to work! Jesus, we have another Dale on our hands.");
                dialogueManager.AddOption(new DialogueOption("SAMMY_UH3", "Yes, ma'am!"));
                dialogueManager.AddOption(new DialogueOption("SAMMY_INQUIREDALE", "Dale?"));
                break;
            case "SAMMY_UH3" :
                dialogueManager.Say("That's more like it!");
                dialogueManager.AddOption(new DialogueOption("SAMMY_END", "..."));
                break;
            case "SAMMY_INQUIREDALE" :
                dialogueManager.Say("Yeah, he works at the front! Lousy piece of shit!");
                dialogueManager.AddOption(new DialogueOption("SAMMY_AGREEDALE", "Yeah, totally!"));
                dialogueManager.AddOption(new DialogueOption("SAMMY_DISAGREEDALE", "No way, Dale's the best!"));
                dialogueManager.AddOption(new DialogueOption("SAMMY_OPEN", "I see..."));
                break;
            case "SAMMY_AGREEDALE" :
                dialogueManager.Say("I'm glad someone agrees with me!");
                dialogueManager.AddOption(new DialogueOption("SAMMY_AGREEDALE2", "Agree, yes."));
                dialogueManager.AddOption(new DialogueOption("SAMMY_AGREEDALE2", "Actually..."));
                dialogueManager.AddOption(new DialogueOption("SAMMY_AGREEDALE2", "..."));
                break;
            case "SAMMY_AGREEDALE2" :
                dialogueManager.Say("Hey, you're not all bad. We should be friends.");
                dialogueManager.AddOption(new DialogueOption("SAMMY_AGREEFRIENDS", "Sure."));
                dialogueManager.AddOption(new DialogueOption("SAMMY_DISAGREEFRIENDS", "Sorry, no."));
                break;
            case "SAMMY_AGREEFRIENDS" :
                dialogueManager.Say("Alright, get back to work!");
                dialogueManager.AddOption(new DialogueOption("SAMMY_END", "Yes, ma'am!"));
                break;
            case "SAMMY_DISAGREEFRIENDS" :
                dialogueManager.Say("Fine, then. Fuck off!");
                dialogueManager.AddOption(new DialogueOption("SAMMY_END", "Gladly."));
                break;
            case "SAMMY_DISAGREEDALE":
                dialogueManager.Say("Oh, yeah? Go talk to him then and stop bothering me.");
                dialogueManager.AddOption(new DialogueOption("SAMMY_END", "Okay."));
                break;
            case "SAMMY_WHOAREYOU" :
                dialogueManager.Say("The name's Sammy, now fuck off!");
                dialogueManager.AddOption(new DialogueOption("SAMMY_END", "Okay."));
                break;
            case "SAMMY_HOWGOESIT" :
                dialogueManager.Say("Not great, actually. There's this idiot in front of me who won't shut up.");
                dialogueManager.AddOption(new DialogueOption("SAMMY_INQUIREANGER", "Are you always this angry?"));
                dialogueManager.AddOption(new DialogueOption("SAMMY_END", "Okay, bye then!"));
                break;
            case "SAMMY_INQUIREANGER" :
                dialogueManager.Say("Anytime someone like you or Dale are around I am!");
                dialogueManager.AddOption(new DialogueOption("SAMMY_INQUIREDALE", "You're angry at Dale?"));
                dialogueManager.AddOption(new DialogueOption("SAMMY_END", "Okay, bye then!"));
                break;
            case "SAMMY_END":
                dialogueManager.Close();
                break;
            default :
                break;
        }
    }

    void Start()
    {
        dialogueManager = DialogueManager.instance;
    }
}
