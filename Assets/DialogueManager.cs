using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    string dialogue = "";
    string options = "";

    int characterCount = 0;
    float currentCooldown = 0.0f;

    public float cooldown = 0.1f;

    public Transform panel;
    public Text dialogueText;
    public Text optionsText;
    public AudioClip characterAudio;

    public List<DialogueOption> dialogueOptions = new List<DialogueOption>();

    public static DialogueManager instance;

    void Awake()
    {
        instance = this;
        panel.gameObject.SetActive(false);
    }

    public void Close()
    {
        panel.gameObject.SetActive(false);
        MessageEventManager.RaiseOnResume();
    }

    public void Say(string msg)
    {
        if (!panel.gameObject.activeInHierarchy)
        {
            panel.gameObject.SetActive(true);
            MessageEventManager.RaiseOnPause();
        }
        characterCount = 0;
        dialogueOptions.Clear();
        dialogue = msg;
    }

    public void AddOption(DialogueOption option)
    {
        dialogueOptions.Add(option);
    }

    void Update()
    {
        // Scroll text one character at a time
        currentCooldown += Time.deltaTime;
        if(currentCooldown >= cooldown) {
            if(dialogueText.text != dialogue) {
                dialogueText.text = dialogue.Substring(0, characterCount);
                if(dialogue.Length >= characterCount + 1 && dialogue[characterCount] != ' ') {
                    AudioSource.PlayClipAtPoint(characterAudio, Camera.main.transform.position);
                }
                characterCount++;
            }
            currentCooldown = 0.0f;
        }

        // Allow player to skip text
        if(Input.GetButtonDown("Skip")) {
            dialogueText.text = dialogue;
        }

        // Construct option text
        string text = "";
        int count = 1;
        foreach(DialogueOption option in dialogueOptions)
        {
            text += count + ". " + option.msg + "   ";
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
