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
        MessageEventManager.RaiseOnReceiveMessage(id + "_OPEN");
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
}
