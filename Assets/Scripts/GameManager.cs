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
    SaveData saveData;
    FlagCollection flagCollection;

    // Singleton instance
    public static GameManager manager;
    
    [Header("Current Game Data")]
    public string filename = null;
    // public List<UnlockedJobData> unlockedJobs = new List<UnlockedJobData>();
    public List<Job> jobs = new List<Job>();

    [Header("Current Job Data")]
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
            if(currentJob != null) {
                SaveJob(currentJob);
            }
            SaveFile();
        }

        if (Input.GetButtonDown("Quickload"))
        {
            if(currentJob != null) {
                LoadJob(currentJob);
            }
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

        saveData.sceneName = SceneManager.GetActiveScene().name;

        // construct flags
        saveData.gameFlags = flagCollection.flags.ToArray();

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

                unlockedJobs = new List<UnlockedJobData>(saveData.unlockedJobs);
            }
            else
            {
                Debug.Log("No save file to load!");
            }

            SceneManager.LoadSceneAsync(saveData.sceneName);
        }

    public void SaveJob(Job job) {
            // construct data
            JobData newJobData = new JobData();

            newJobData.id = job.id;

            // construct flags
            newJobData.flags = currentJob.flagCollection.flags.ToArray();

            // construct PlayerData
            PlayerData playerData = new PlayerData();
            playerData.position = new float[2] { currentJob.playerObject.transform.position.x, currentJob.playerObject.transform.position.y };
            newJobData.player = playerData;

            // construct NPCDatas
            List<NPCData> NPCDatas = new List<NPCData>();
            foreach(NPC npc in currentJob.NPCs)
            {
                NPCData data = new NPCData();

                data.id = npc.id;
                data.position = new float[2] { npc.transform.position.x, npc.transform.position.y };
                data.flags = npc.flagCollection.flags.ToArray();
                NPCDatas.Add(data);
            }
            newJobData.npcs = NPCDatas.ToArray();

            // construct MinigameDatas
            List<MinigameData> minigameDatas = new List<MinigameData>();
            foreach (Minigame minigame in currentJob.minigames)
            {
                MinigameData data = new MinigameData();

                data.id = minigame.id;
                data.rating = minigame.bestRating;
                data.score = minigame.bestScore;
                minigameDatas.Add(data);
            }
            newJobData.minigames = minigameDatas.ToArray();

            newJobData.day = job.currentDay;
            newJobData.time = job.currentTimeChunk;

            bool jobExists = false;
            for(int i = 0; i < saveData.jobs.Length; i++ ) {
                if(saveData.jobs[i].id == newJobData.id) {
                    saveData.jobs[i] = newJobData;
                    jobExists = true;
                }
            }

            if(!jobExists) {
                List<JobData> newJobs = new List<JobData>(saveData.jobs);
                newJobs.Add(newJobData);
                saveData.jobs = newJobs.ToArray();
            }
        }

    public void LoadJob(Job job) {
        foreach(JobData jobData in saveData.jobs) {
            if(job.id == jobData.id) {

                // load job flags
                job.flagCollection.flags = new List<string>(saveData.gameFlags);

                // load PlayerData
                currentJob.playerObject.transform.position = new Vector3(jobData.player.position[0], jobData.player.position[1], 0f);

                // load NPCDatas
                foreach (NPC npc in currentJob.NPCs)
                {
                    foreach(NPCData data in jobData.npcs)
                    {
                        if (data.id == npc.id)
                        {
                            npc.transform.position = new Vector3(data.position[0], data.position[1], 0f);
                            npc.flagCollection.flags = new List<string>(data.flags);
                        }
                    }
                }

                // load MinigameDatas
                foreach (Minigame minigame in currentJob.minigames)
                {
                    foreach (MinigameData data in jobData.minigames)
                    {
                        if (data.id == minigame.id)
                        {
                            minigame.bestRating = data.rating;
                            minigame.bestScore = data.score;
                        }
                    }
                }

                job.currentDay = jobData.day;
                job.currentTimeChunk = jobData.time;
                
            }
        }
    }

    public void Quickload() {
        int filenameCount = 0;
        while(File.Exists(Path.Combine(Application.persistentDataPath, "saves", "tempsave" + filenameCount + ".dat"))) {
            filenameCount++;
        }
        filenameCount--;
        LoadFile("tempsave" + filenameCount + ".dat");
    }

    #endregion

    #region Database Registration

    public void RegisterJob(Job job)
    {
        currentJob = job;
        LoadJob(currentJob);
    }

    #endregion

    public void StartNewGame()
    {
        saveData = new SaveData();
        unlockedJobs.Add(newGameUnlockedJob);
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
        StartCoroutine(NewDayPopup(3f));
    }

    IEnumerator NewDayPopup(float length)
    {
        MessageEventManager.RaiseOnPause(true, true);
        newDayPopup.SetActive(true);
        newDayText.text = "Day " + currentDay;
        GameManager.manager.MovePlayerToSpawn();
        for (float count = 0f; count <= length; count += Time.deltaTime)
        {
            yield return null;
        }
        newDayPopup.SetActive(false);
        MessageEventManager.RaiseOnResume();
    }

    public bool CheckComplete()
    {
        return false;
    }

    public void MovePlayerToSpawn()
    {
        currentJob.playerObject.transform.position = currentJob.playerSpawn.position;
    }

    public void LeaveJob(bool isSaving)
    {
        MovePlayerToSpawn();
        if (isSaving)
        {
            if (currentJob != null)
            {
                SaveJob(currentJob);
            }
            SaveFile();
        }

        SceneManager.LoadSceneAsync(mapScene);
    }

    public void QuitGame(bool isSaving)
    {
        MovePlayerToSpawn();
        if (isSaving)
        {
            if (currentJob != null)
            {
                SaveJob(currentJob);
            }
            SaveFile();
        }

        saveData = null;
        SceneManager.LoadSceneAsync(mainMenuScene);
    }
}

[Serializable]
class SaveData
{
    public string sceneName;
    public string[] gameFlags;
    public JobData[] jobs;

    public SaveData() {
        gameFlags = new string[0];
        jobs = new JobData[0];
    }
}

[Serializable]
class JobData
{
    public string id;
    public PlayerData player;
    public NPCData[] npcs;
    public MinigameData[] minigames;
    public int day;
    public int time;
    public string[] flags;
}

[Serializable]
class PlayerData
{
    public float[] position;
}

[Serializable]
class NPCData
{
    public string id;
    public float[] position;
    public string[] flags;
}

[Serializable]
class MinigameData
{
    public string id;
    public int score;
    public int rating;
}