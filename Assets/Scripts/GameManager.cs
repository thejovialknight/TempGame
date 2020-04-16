using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    #region Properties

    // Singleton instance
    public static GameManager instance;
    
    [Header("Current Game Data")]
    public SaveData saveData;
    public string filename = null;
    public Job currentJob;
    public List<Item> items = new List<Item>();
    public string activeItem = null;
    public FlagCollection flagCollection = new FlagCollection();

    [Header("New Game Settings")]
    public string newGameScene = "Map";


    [Header("Misc Scenes")]
    public string mainMenuScene = "MainMenu";
    public string mapScene = "Map";

    #endregion

    #region MonoBehaviours

    void Awake()
    {
        // Sets up singleton
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Saving and Loading

    public void SaveFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "saves", filename + ".sav"));

        // save scene name
        saveData.currentSceneName = SceneManager.GetActiveScene().name;

        // save flags
        saveData.gameFlags = flagCollection.flags.ToArray();

        // save job data
        if(currentJob != null) {
            for(int i = 0; i < saveData.jobs.Length; i++) {
                if(currentJob.id == saveData.jobs[i].id) {
                    saveData.jobs[i] = currentJob.SaveData();
                }
            }
        }

        saveData.items = new string[items.Count];
        if(items.Count > 0) {
            for(int i = 0; i < saveData.items.Length; i++) {
                saveData.items[i] = items[i].id;
            }
        }

        // serialize
        bf.Serialize(file, saveData);
        file.Close();

        Debug.Log("FILESAVED!");
    }

    public void LoadFile(string fName)
    {
        // check if saves folder exists
        if(!Directory.Exists(Path.Combine(Application.persistentDataPath, "saves")))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "saves"));
        }

        if(File.Exists(Path.Combine(Application.persistentDataPath, "saves", fName)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "saves", fName), FileMode.Open);

            //deserialize
            saveData = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("No save file to load!");
        }

        flagCollection.flags = new List<string>(saveData.gameFlags);
        items = ItemDatabase.instance.GetItemsFromIDs(saveData.items);
        SceneManager.LoadSceneAsync(saveData.currentSceneName);
    }

    public void LoadJob() {
        if(currentJob != null) {
            if(saveData == null) {
                saveData = new SaveData();
            }

            foreach(JobData jobData in saveData.jobs) {
                if(jobData.id == currentJob.id) {
                    currentJob.LoadData(jobData);
                }
            }
        }
    }

    void ConstructJobs() {
        saveData.jobs = new JobData[1];
        saveData.jobs[0] = new JobData { id = "POST_OFFICE", sceneName = "Job_PostOffice", name = "Post Office", isUnlocked = true };
    }

    #endregion

    #region Database Registration

    public void RegisterJob(Job job)
    {
        currentJob = job;
        MessageEventManager.RegisterJob();
        LoadJob();
    }

    public void RegisterPlayer(PlayerController player)
    {
        currentJob.player = player;
    }

    public void RegisterNPC(NPC npc)
    {
        currentJob.npcs.Add(npc);
    }

    public void RegisterMinigame(Minigame minigame)
    {
        currentJob.minigames.Add(minigame);
    }

    #endregion

    public void StartNewGame()
    {
        filename = "Conner";
        saveData = new SaveData();
        ConstructJobs();
        SceneManager.LoadSceneAsync(newGameScene);
    }

    public void ProgressTime()
    {
        currentJob.currentTimeChunk++;
    }

    public void ProgressDay()
    {
        currentJob.currentDay++;
        currentJob.currentTimeChunk = 0;
        // send message which new day pop up is listening for
    }

    public void SetJobFlag(string id, bool isOn) {
        currentJob.flagCollection.SetFlag(id, isOn);
    }

    public bool CheckJobFlag(string id) {
        return currentJob.flagCollection.CheckFlag(id);
    }

    public void MovePlayerToSpawn()
    {
        currentJob.player.transform.position = currentJob.playerSpawn.position;
    }

    public void LeaveJob(bool isSaving)
    {
        MovePlayerToSpawn();
        if (isSaving)
        {
            SaveFile();
        }

        SceneManager.LoadSceneAsync(mapScene);
    }

    public void QuitGame(bool isSaving)
    {
        MovePlayerToSpawn();
        if (isSaving)
        {
            SaveFile();
        }

        SceneManager.LoadSceneAsync(mainMenuScene);

        saveData = null;
        items.Clear();
        currentJob = null;
        filename = null;
        flagCollection.Clear();
    }

    public void SetActiveItem(Item item)
    {
        activeItem = item.id;
        if(currentJob != null && currentJob.player != null)
        {
            currentJob.player.carriedSprite = item.worldSprite;
        }
    }

    public void ClearActiveItem()
    {
        activeItem = null;
        if (currentJob != null && currentJob.player != null)
        {
            currentJob.player.carriedSprite = null;
        }
    }

    public void AddItem(Item item) {
        items.Add(item);
        SetActiveItem(item);
    }

    public void RemoveItem(string id) {
        if(items.Exists(x => x.id == id)) {
            Item item = items.Find(x => x.id == id);
            if(item.id == activeItem)
            {
                ClearActiveItem();
            }
            items.Remove(item);
        }
    }
}

[Serializable]
public class SaveData
{
    public string currentSceneName;
    public string[] gameFlags;
    public JobData[] jobs;
    public string[] items;

    public SaveData() {
        gameFlags = new string[0];
        jobs = new JobData[0];
        items = new string[0];
    }
}