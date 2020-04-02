using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public string filename = null;
    public bool isLoadingJob = false;

    public static GameManager manager;

    public Job startingJob;
    public string startingScene;
    public List<UnlockedJobData> unlockedJobs = new List<UnlockedJobData>();
    public List<Job> jobs = new List<Job>();
    public Job currentJob;
    public GameObject playerObject;
    public List<NPC> NPCs = new List<NPC>();
    public List<Minigame> minigames = new List<Minigame>();

    SaveData saveData;
    FlagCollection flagCollection;

    void Awake()
    {
        flagCollection = GetComponent<FlagCollection>();

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

        if(isLoadingJob) {
            if(currentJob != null) {
                LoadJob(currentJob);
            }
            else {
                Debug.Log("No current job to load into!");
            }
            isLoadingJob = false;
        }
    }

    public void SaveJob(Job job) {
        // construct data
        JobData newJobData = new JobData();

        newJobData.id = job.id;

        // construct flags
        newJobData.flags = currentJob.flagCollection.flags.ToArray();

        // construct PlayerData
        PlayerData playerData = new PlayerData();
        playerData.position = new float[2] { playerObject.transform.position.x, playerObject.transform.position.y };
        newJobData.player = playerData;

        // construct NPCDatas
        List<NPCData> NPCDatas = new List<NPCData>();
        foreach(NPC npc in NPCs)
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
        foreach (Minigame minigame in minigames)
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

    public void StartNewGame() {
        saveData = new SaveData();
        unlockedJobs.Add(new UnlockedJobData("Job_PostOffice", "Post Office"));
        SceneManager.LoadSceneAsync(startingScene);
    }

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

        saveData.unlockedJobs = unlockedJobs.ToArray();

        // serialize
        bf.Serialize(file, saveData);
        file.Close();

        Debug.Log("FILESAVED!");
    }

    public void Quickload() {
        int filenameCount = 0;
        while(File.Exists(Path.Combine(Application.persistentDataPath, "saves", "tempsave" + filenameCount + ".dat"))) {
            filenameCount++;
        }
        filenameCount--;
        LoadFile("tempsave" + filenameCount + ".dat");
    }

    public void LoadJob(Job job) {
        foreach(JobData jobData in saveData.jobs) {
            if(job.id == jobData.id) {

                // load job flags
                job.flagCollection.flags = new List<string>(saveData.gameFlags);

                // load PlayerData
                playerObject.transform.position = new Vector3(jobData.player.position[0], jobData.player.position[1], 0f);

                // load NPCDatas
                foreach (NPC npc in NPCs)
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
                foreach (Minigame minigame in minigames)
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

    public void RegisterJob(Job job)
    {
        currentJob = job;
    }

    public void RegisterPlayer(GameObject player)
    {
        playerObject = player;
    }

    public void RegisterNPC(NPC npc)
    {
        NPCs.Add(npc);
    }

    public void RegisterMinigame(Minigame minigame)
    {
        minigames.Add(minigame);
    }
}

[Serializable]
class SaveData
{
    public string sceneName;
    public string[] gameFlags;
    public JobData[] jobs;
    public UnlockedJobData[] unlockedJobs;

    public SaveData() {
        gameFlags = new string[0];
        jobs = new JobData[0];
    }
}

[Serializable]
public class UnlockedJobData
{
    public string sceneName;
    public string title;

    public UnlockedJobData(string sceneName, string title) {
        this.sceneName = sceneName;
        this.title = title;
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