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
        MessageEventManager.OnDialogue += OnDialogue;
        MessageEventManager.OnCutscene += OnCutscene;
        MessageEventManager.OnJobRegister += OnJobRegister;
    }

    void OnDisable()
    {
        GameManager.OnPause -= OnPause;
        GameManager.OnResume -= OnResume;
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
        MessageEventManager.Dialogue(id, "OPEN");
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
        GameManager.instance.RegisterNPC(this);
    }

    public virtual void OnDialogue(string id, string message)
    {
        
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

    public void Say(string text)
    {
        DialogueManager.instance.Say(text);
    }

    public void AddOption(string text, string node)
    {
        DialogueManager.instance.AddOption(new DialogueOption(node, id, text));
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
    public FlagData flags;
}