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

    // Private Properties
    public SaveData saveData;
    public FlagCollection flagCollection = new FlagCollection();

    // Singleton instance
    public static GameManager manager;
    
    [Header("Current Game Data")]
    public string filename = null;
    public Job currentJob;


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
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if(manager != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Handles input
        if(Input.GetButtonDown("Quicksave"))
        {
            SaveFile();
        }

        if (Input.GetButtonDown("Quickload"))
        {
            Quickload();
        }
    }

    #endregion

    #region Saving and Loading

    public void SaveFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        int filenameCount = 0;
        while(File.Exists(Path.Combine(Application.persistentDataPath, "saves", "tempsave" + filenameCount + ".dat"))) {
            filenameCount++;
        }
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "saves", "tempsave" + filenameCount + ".dat"));

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

        SceneManager.LoadSceneAsync(saveData.currentSceneName);
    }

    public void Quickload() {
        int filenameCount = 0;
        while(File.Exists(Path.Combine(Application.persistentDataPath, "saves", "tempsave" + filenameCount + ".dat"))) {
            filenameCount++;
        }
        filenameCount--;
        LoadFile("tempsave" + filenameCount + ".dat");
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
        MessageEventManager.RaiseRegisterJob();
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
        saveData = new SaveData();
        ConstructJobs();
        SceneManager.LoadSceneAsync(newGameScene);
    }

    void ProgressTime()
    {
        currentJob.currentTimeChunk++;
    }

    public void ProgressDay()
    {
        currentJob.currentDay++;
        currentJob.currentTimeChunk = 0;
        // send message which new day pop up is listening for
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

        saveData = null;
        SceneManager.LoadSceneAsync(mainMenuScene);
    }
}

[Serializable]
public class SaveData
{
    public string currentSceneName;
    public string[] gameFlags;
    public JobData[] jobs;

    public SaveData() {
        gameFlags = new string[0];
        jobs = new JobData[0];
    }
}