using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string id = "NAME";
    public string title = "Name";
    public string jobTitle = "Job Title";

    public FlagCollection flagCollection;

    void OnEnable()
    {
        MessageEventManager.OnReceiveMessageEvent += OnReceiveMessage;
    }

    void OnDisable()
    {
        MessageEventManager.OnReceiveMessageEvent -= OnReceiveMessage;
    }

    void Awake()
    {
        flagCollection = GetComponent<FlagCollection>();
    }

    void Start()
    {
        GameManager.manager.RegisterNPC(this);
    }

    public void InteractWith(Transform interactor)
    {
        MessageEventManager.Broadcast(id + "_OPEN");
    }

    public string GetInteractName()
    {
        return title + " (" + jobTitle + ")";
    }

    public string GetInteractInfo()
    {
        return "Press E to talk";
    }

    public virtual void OnReceiveMessage(string id)
    {

    }

    public void SetFlag(string flag, bool isOn)
    {
        flagCollection.SetFlag(flag, isOn);
    }

    public bool CheckFlag(string flag)
    {
        return flagCollection.CheckFlag(flag);
    }

    public void Broadcast(string node)
    {
        MessageEventManager.Broadcast(id + node);
    }

    public void StartCutscene(string node)
    {
        MessageEventManager.BroadcastCutscene(node);
    }

    public bool CheckMessage(string message, string node)
    {
        if(message == id + "_" + node)
        {
            return true;
        }
        return false;
    }

    public bool CheckCutsceneMessage(string message, string cutscene)
    {
        if (message == "CUTSCENE_" + cutscene)
        {
            return true;
        }
        return false;
    }

    public void Say(string text)
    {
        DialogueManager.instance.Say(text);
    }

    public void AddOption(string text, string node)
    {
        DialogueManager.instance.AddOption(new DialogueOption(id + "_" + node, text));
    }

    public void CloseDialogue()
    {
        DialogueManager.instance.Close();
    }
}
