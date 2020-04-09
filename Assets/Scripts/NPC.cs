using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string id;
    public string title;
    public string jobTitle;

    public bool isPaused = false;

    public FlagCollection flagCollection = new FlagCollection();

    void OnEnable()
    {
        MessageEventManager.OnPauseEvent += OnPause;
        MessageEventManager.OnResumeEvent += OnResume;
        MessageEventManager.OnReceiveMessageEvent += OnReceiveMessage;
        MessageEventManager.OnRegisterJobEvent += OnRegisterJob;
    }

    void OnDisable()
    {
        MessageEventManager.OnPauseEvent -= OnPause;
        MessageEventManager.OnResumeEvent -= OnResume;
        MessageEventManager.OnReceiveMessageEvent -= OnReceiveMessage;
        MessageEventManager.OnRegisterJobEvent -= OnRegisterJob;
    }

    public void LoadData(NPCData dataToLoad) {
        name = dataToLoad.name;
        jobTitle = dataToLoad.jobTitle;
        if(dataToLoad.hasBeenSaved) {
            transform.position = new Vector3(dataToLoad.position[0], dataToLoad.position[1], 0f);
            flagCollection.flags = new List<string>(dataToLoad.flags);
        }
    }

    public NPCData SaveData() {
        NPCData dataToSave = new NPCData();
        dataToSave.hasBeenSaved = true;
        dataToSave.id = id;
        dataToSave.name = title;
        dataToSave.jobTitle = jobTitle;
        dataToSave.position = new float[2] { transform.position.x, transform.position.y };
        dataToSave.flags = flagCollection.flags.ToArray();
        return dataToSave;
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

    void OnPause(bool pausePlayer, bool pauseNPCs, params NPC[] exceptions)
    {
        isPaused = true;
        if (pauseNPCs)
        {
            foreach(NPC npc in exceptions)
            {
                if(npc == this)
                {
                    isPaused = false;
                }
            }
        }
    }

    void OnResume()
    {
        isPaused = false;
    }

    void OnRegisterJob() {
        GameManager.manager.RegisterNPC(this);
    }

    public virtual void OnReceiveMessage(string message)
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
        MessageEventManager.Broadcast(id + "_" + node);
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

[System.Serializable]
public class NPCData
{
    public bool hasBeenSaved;
    public string id;
    public string name;
    public string jobTitle;
    public float[] position;
    public string[] flags;
}