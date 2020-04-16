using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public string id = "EXIT_DOOR";

    void OnEnable()
    {
        MessageEventManager.OnDialogue += OnDialogue;
    }

    void OnDisable()
    {
        MessageEventManager.OnDialogue -= OnDialogue;
    }

    void OnDialogue(string id, string message)
    {
        if(id != this.id) {
            return;
        }

        if(message == "DAY")
        {
            DialogueManager.instance.Close();
            GameManager.instance.ProgressDay();
        }

        if (message == "JOB")
        {
            DialogueManager.instance.Close();
            GameManager.instance.LeaveJob(true);
        }

        if (message == "GAME")
        {
            DialogueManager.instance.Close();
            GameManager.instance.QuitGame(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DialogueManager.instance.Say("Exit Door:");
            DialogueManager.instance.AddOption(new DialogueOption("DAY", id, "End Day"));
            DialogueManager.instance.AddOption(new DialogueOption("JOB", id, "Leave Job"));
            DialogueManager.instance.AddOption(new DialogueOption("GAME", id, "Save and Quit"));
        }
    }
}
