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
        GameManager.OnPause += OnPause;
        GameManager.OnResume += OnResume;
        GameManager.OnProgressDay += OnProgressDay;
        GameManager.OnProgressTime += OnProgressTime;

        MessageEventManager.OnDialogue += OnDialogue;
        MessageEventManager.OnCutscene += OnCutscene;
        MessageEventManager.OnJobRegister += OnJobRegister;
    }

    void OnDisable()
    {
        GameManager.OnPause -= OnPause;
        GameManager.OnResume -= OnResume;
        GameManager.OnProgressDay -= OnProgressDay;
        GameManager.OnProgressTime -= OnProgressTime;

        MessageEventManager.OnDialogue -= OnDialogue;
        MessageEventManager.OnCutscene += OnCutscene;
        MessageEventManager.OnJobRegister -= OnJobRegister;
    }

    public void LoadData(NPCData dataToLoad) {
        name = dataToLoad.name;
        jobTitle = dataToLoad.jobTitle;
        if(dataToLoad.hasBeenSaved) {
            transform.position = new Vector3(dataToLoad.position[0], dataToLoad.position[1], 0f);
            flagCollection.LoadData(dataToLoad.flags);
        }
    }

    public NPCData SaveData() {
        NPCData dataToSave = new NPCData();
        dataToSave.hasBeenSaved = true;
        dataToSave.id = id;
        dataToSave.name = title;
        dataToSave.jobTitle = jobTitle;
        dataToSave.position = new float[2] { transform.position.x, transform.position.y };
        dataToSave.flags = flagCollection.SaveData();
        return dataToSave;
    }

    public void InteractWith(Transform interactor)
    {
        DialogueManager.instance.GotoNode("OPEN", id);
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

    void OnJobRegister() {
        GameManager.RegisterNPC(this);
    }

    public virtual void OnProgressDay(int day) {

    }

    public virtual void OnProgressTime(int time) {

    }

    public void OnDialogue(string id, string message, params string[] args)
    {
        if(id != this.id) {
            return;
        }

        if (message == "CLOSE")
        {
            DialogueManager.instance.Close();
            return;
        }

        HandleDialogue(message, args);
    }

    public virtual void HandleDialogue(string message, string[] args) {

    }

    public virtual void OnCutscene(string message)
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
}

[System.Serializable]
public class NPCData
{
    public bool hasBeenSaved;
    public string id;
    public string name;
    public string jobTitle;
    public float[] position;
    public FlagData flags;
}