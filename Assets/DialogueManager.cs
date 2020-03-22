using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text optionsText;

    public List<DialogueOption> dialogueOptions = new List<DialogueOption>();

    public static DialogueManager instance;

    void Awake()
    {
        instance = this;
    }

    public void Say(string msg)
    {
        dialogueOptions.Clear();
        dialogueText.text = "Dialogue: " + msg;
    }

    public void AddOption(DialogueOption option)
    {
        dialogueOptions.Add(option);
    }

    void Update()
    {
        string text = "";
        int count = 1;
        foreach(DialogueOption option in dialogueOptions)
        {
            text += count + ". " + option.msg + " (" + option.id + ") ";
            count++;
        }

        optionsText.text = text;

        int keyPressed = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            keyPressed = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            keyPressed = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            keyPressed = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            keyPressed = 4;
        }

        if(keyPressed != -1 && dialogueOptions.Count >= keyPressed)
        {
            MessageEventManager.RaiseOnReceiveMessage(dialogueOptions[keyPressed - 1].id);
        }
    }
}
