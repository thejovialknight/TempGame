using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    void OnEnable()
    {
        MessageEventManager.OnReceiveMessageEvent += OnReceiveMessage;
    }

    void OnDisable()
    {
        MessageEventManager.OnReceiveMessageEvent -= OnReceiveMessage;
    }

    void OnReceiveMessage(string message)
    {
        if(message == "EXITDOOR_DAY")
        {
            DialogueManager.instance.Close();
            GameManager.manager.ProgressDay();
        }

        if (message == "EXITDOOR_JOB")
        {
            DialogueManager.instance.Close();
            GameManager.manager.LeaveJob(true);
        }

        if (message == "EXITDOOR_GAME")
        {
            DialogueManager.instance.Close();
            GameManager.manager.QuitGame(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DialogueManager.instance.Say("Exit Door:");
            DialogueManager.instance.AddOption(new DialogueOption("EXITDOOR_DAY", "End Day"));
            DialogueManager.instance.AddOption(new DialogueOption("EXITDOOR_JOB", "Leave Job"));
            DialogueManager.instance.AddOption(new DialogueOption("EXITDOOR_GAME", "Save and Quit"));
        }
    }
}
