using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    string dialogue = "";

    int characterCount = 0;
    float currentCooldown = 0.0f;

    public float cooldown = 0.1f;

    public Transform panel;
    public TextMeshProUGUI dialogueText;
    public AudioClip characterAudio;
    public Transform optionsContentTransform;
    public GameObject optionButtonPrefab;

    public List<DialogueOption> dialogueOptions = new List<DialogueOption>();

    public static DialogueManager instance;

    void OnEnable()
    {
        DialogueOptionButton.OnDialoguePressed += OnDialoguePressed;
    }

    void OnDisable()
    {
        DialogueOptionButton.OnDialoguePressed -= OnDialoguePressed;
    }

    void Awake()
    {
        instance = this;
        panel.gameObject.SetActive(false);
    }

    void OnDialoguePressed(DialogueOption option)
    {
        MessageEventManager.Dialogue(option.receiverID, option.nodeID, option.args);
    }

    public void Close()
    {
        dialogue = "";
        dialogueText.text = "";
        ClearOptions();
        panel.gameObject.SetActive(false);
        GameManager.Resume();
    }

    public void Say(string msg)
    {
        if (!panel.gameObject.activeInHierarchy)
        {
            panel.gameObject.SetActive(true);
            GameManager.Pause(true, true);
        }
        characterCount = 0;
        ClearOptions();
        dialogue = msg;
    }

    public void GotoNode(string nodeID, string receiverID, params string[] args)
    {
        MessageEventManager.Dialogue(receiverID, nodeID, args);
    }

    public void GotoNode(string nodeID, string receiverID)
    {
        GotoNode(nodeID, receiverID, null);
    }

    public void AddOption(DialogueOption option)
    {
        dialogueOptions.Add(option);
        GameObject optionButton = GameObject.Instantiate(optionButtonPrefab, optionsContentTransform);
        optionButton.GetComponent<DialogueOptionButton>().Init(option);
    }

    public void AddOption(string nodeID, string receiverID, string message, params string[] args)
    {
        AddOption(new DialogueOption(nodeID, receiverID, message, args));
    }

    public void AddOption(string nodeID, string receiverID, string message)
    {
        AddOption(new DialogueOption(nodeID, receiverID, message, null));
    }

    public void ClearOptions()
    {
        foreach (Transform child in optionsContentTransform)
        {
            dialogueOptions.Clear();
            GameObject.Destroy(child.gameObject);
        }
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
    }
}
