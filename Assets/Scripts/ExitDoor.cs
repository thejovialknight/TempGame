using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public string id = "EXIT_DOOR";
    public Vector2 direction;

    void OnEnable()
    {
        MessageEventManager.OnDialogue += OnDialogue;
    }

    void OnDisable()
    {
        MessageEventManager.OnDialogue -= OnDialogue;
    }

    void OnDialogue(string id, string message, params string[] args)
    {
        if(id != this.id) {
            return;
        }

        if(message == "DAY")
        {
            DialogueManager.instance.Close();
            GameManager.ProgressDay();
        }

        if (message == "JOB")
        {
            DialogueManager.instance.Close();
            GameManager.LeaveJob(true);
        }

        if (message == "GAME")
        {
            DialogueManager.instance.Close();
            GameManager.QuitGame(true);
        }

        if (message == "CANCEL")
        {
            StartCoroutine(MovePlayer(0.4f));
            DialogueManager.instance.Close();
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
            DialogueManager.instance.AddOption(new DialogueOption("CANCEL", id, "[X] Return to Job"));
        }
    }

    IEnumerator MovePlayer(float length)
    {
        PlayerController player = GameManager.Player;
        GameManager.Pause(true, false);
        while (length > 0f)
        {
            player.freeMovement.Move(direction);
            length -= Time.deltaTime;
            yield return null;
        }
        GameManager.Resume();
    }
}
