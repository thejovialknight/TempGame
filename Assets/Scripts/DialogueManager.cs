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

    float desiredOptionsPos;
    float desiredOptionsSize;
    float desiredPanelSize;

    public float lerpSpeed;

    public Transform panel;
    public TextMeshProUGUI dialogueText;
    public AudioClip characterAudio;
    public Transform optionsScrollListTransform;
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
        desiredPanelSize = 0f;
        desiredOptionsSize = 0f;
    }

    void Awake()
    {
        instance = this;
        panel.gameObject.SetActive(false);
    }

    void ValidateSize() {
        desiredPanelSize = 50f;
        desiredOptionsPos = -desiredPanelSize + 5f;

        desiredOptionsSize = 0f;
        foreach (Transform child in optionsContentTransform)
        {
            desiredOptionsSize += 21;
        }

        desiredOptionsSize = Mathf.Clamp(desiredOptionsSize, 0f, 84f);
        desiredPanelSize += desiredOptionsSize;
    }

    void OnDialoguePressed(DialogueOption option)
    {
        MessageEventManager.Dialogue(option.receiverID, option.nodeID, option.args);
    }

    public static void Close()
    {
        DialogueManager.instance.dialogue = "";
        DialogueManager.instance.dialogueText.text = "";
        ClearOptions();
        DialogueManager.instance.panel.gameObject.SetActive(false);
        GameManager.Resume();
        DialogueManager.instance.desiredPanelSize = 0f;
        DialogueManager.instance.desiredOptionsSize = 0f;
    }

    public static void Say(string msg)
    {
        if (!DialogueManager.instance.panel.gameObject.activeInHierarchy)
        {
            DialogueManager.instance.panel.gameObject.SetActive(true);

            GameManager.State = GameState.Dialogue;
            GameManager.Pause(true, true);
        }
        DialogueManager.instance.characterCount = 0;
        ClearOptions();
        DialogueManager.instance.dialogue = msg;
    }

    public static void GotoNode(string nodeID, string receiverID, params string[] args)
    {
        MessageEventManager.Dialogue(receiverID, nodeID, args);
    }

    public static void GotoNode(string nodeID, string receiverID)
    {
        GotoNode(nodeID, receiverID, new string[0]);
    }

    public static void AddOption(DialogueOption option)
    {
        DialogueManager.instance.dialogueOptions.Add(option);
        GameObject optionButton = GameObject.Instantiate(DialogueManager.instance.optionButtonPrefab, DialogueManager.instance.optionsContentTransform);
        optionButton.GetComponent<DialogueOptionButton>().Init(option);
    }

    public static void AddOption(string nodeID, string receiverID, string message, params string[] args)
    {
        AddOption(new DialogueOption(nodeID, receiverID, message, args));
    }

    public static void AddOption(string nodeID, string receiverID, string message)
    {
        AddOption(new DialogueOption(nodeID, receiverID, message, new string[0]));
    }

    public static void ClearOptions()
    {
        foreach (Transform child in DialogueManager.instance.optionsContentTransform)
        {
            DialogueManager.instance.dialogueOptions.Clear();
            GameObject.Destroy(child.gameObject);
        }
    }

    public static string CheckArg(string[] args, int argToCheck)
    {
        if(args.Length > argToCheck)
        {
            return args[argToCheck];
        }
        return null;
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

        if(panel.gameObject.activeInHierarchy) {
            ValidateSize();
        }

        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.sizeDelta = Vector2.Lerp(panelRect.sizeDelta, new Vector2(625f, desiredPanelSize), lerpSpeed * Time.deltaTime);

        RectTransform optionsRect = optionsScrollListTransform.GetComponent<RectTransform>();
        optionsRect.anchoredPosition = Vector2.Lerp(optionsRect.anchoredPosition, new Vector2(0f, desiredOptionsPos), lerpSpeed * Time.deltaTime);
        optionsRect.sizeDelta = Vector2.Lerp(optionsRect.sizeDelta, new Vector2(592f, desiredOptionsSize), lerpSpeed * Time.deltaTime);
    }
}
