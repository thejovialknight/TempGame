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
    static GameManager instance;
    
    [Header("Current Game Data")]
    [SerializeField]
    SaveData saveData;
    [SerializeField]
    string filename = null;
    [SerializeField]
    Job currentJob;
    [SerializeField]
    List<Item> inventory = new List<Item>();
    [SerializeField]
    Item activeItem = null;
    [SerializeField]
    FlagCollection flagCollection = new FlagCollection();
    [SerializeField]
    GameState gameState = GameState.Default;

    [Header("New Game Settings")]
    public string newGameScene = "Map";

    [Header("Misc Scenes")]
    public string mainMenuScene = "MainMenu";
    public string mapScene = "Map";

    #endregion

    #region Get/Set

    public static SaveData SaveData
    {
        get
        {
            if (GameManager.instance != null)
            {
                return GameManager.instance.saveData;
            }
            return null;
        }

        set
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.saveData = value;
            }
        }
    }

    public static string Filename
    {
        get
        {
            if (GameManager.instance != null)
            {
                return GameManager.instance.filename;
            }
            return null;
        }

        set
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.filename = value;
            }
        }
    }

    public static List<Item> Inventory
    {
        get
        {
            if (GameManager.instance != null)
            {
                return GameManager.instance.inventory;
            }
            return null;
        }

        set
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.inventory = value;
            }
        }
    }

    public static Item ActiveItem
    {
        get
        {
            if (GameManager.instance != null)
            {
                return GameManager.instance.activeItem;
            }
            return null;
        }

        set
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.activeItem = value;
            }
        }
    }

    static Job Job
    {
        get
        {
            if (GameManager.instance != null)
            {
                return GameManager.instance.currentJob;
            }
            return null;
        }

        set
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.currentJob = value;
            }
        }
    }

    public static FlagCollection Flags
    {
        get
        {
            if (GameManager.instance != null)
            {
                return GameManager.instance.flagCollection;
            }
            return null;
        }

        set
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.flagCollection = value;
            }
        }
    }

    public static FlagCollection JobFlags
    {
        get
        {
            if (Job != null)
            {
                return Job.flagCollection;
            }
            return null;
        }

        set
        {
            if (Job != null)
            {
                Job.flagCollection = value;
            }
        }
    }

    public static GameState State
    {
        get
        {
            if (GameManager.instance != null)
            {
                return GameManager.instance.gameState;
            }
            return GameState.Default;
        }

        set
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.gameState = value;
            }
        }
    }

    public static PlayerController Player
    {
        get
        {
            if (Job != null)
            {
                return Job.player;
            }
            return null;
        }

        set
        {
            if (Job != null)
            {
                Job.player = value;
            }
        }
    }

    public static string JobTitle
    {
        get
        {
            if (Job != null)
            {
                return Job.title;
            }
            return null;
        }

        set
        {
            if (Job != null)
            {
                Job.title = value;
            }
        }
    }

    public static int Day
    {
        get
        {
            if (Job != null)
            {
                return Job.currentDay;
            }
            return 0;
        }

        set
        {
            if (Job != null)
            {
                Job.currentDay = value;
            }
        }
    }

    public static int TimeChunk
    {
        get
        {
            if (Job != null)
            {
                return Job.currentTimeChunk;
            }
            return 0;
        }

        set
        {
            if (Job != null)
            {
                Job.currentTimeChunk = value;
            }
        }
    }

    public static string TimeChunkString
    {
        get
        {
            if (Job != null)
            {
                return Job.timeChunks[Job.currentTimeChunk];
            }
            return null;
        }

        set
        {
            if (Job != null)
            {
                Job.timeChunks[Job.currentTimeChunk] = value;
            }
        }
    }

    public static List<NPC> NPCs
    {
        get
        {
            if (Job != null)
            {
                return Job.npcs;
            }
            return null;
        }

        set
        {
            if (Job != null)
            {
                Job.npcs = value;
            }
        }
    }

    public static List<Minigame> Minigames
    {
        get
        {
            if (Job != null)
            {
                return Job.minigames;
            }
            return null;
        }

        set
        {
            if (Job != null)
            {
                Job.minigames = value;
            }
        }
    }

    public static List<Container> Containers
    {
        get
        {
            if (Job != null)
            {
                return Job.containers;
            }
            return null;
        }

        set
        {
            if (Job != null)
            {
                Job.containers = value;
            }
        }
    }

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

    void SaveFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "saves", filename + ".sav"));

        // save scene name
        saveData.currentSceneName = SceneManager.GetActiveScene().name;

        // save flags
        saveData.gameFlags = flagCollection.SaveData();

        // save job data
        if(currentJob != null) {
            for(int i = 0; i < saveData.jobs.Length; i++) {
                if(currentJob.id == saveData.jobs[i].id) {
                    saveData.jobs[i] = currentJob.SaveData();
                }
            }
        }

        saveData.items = new string[inventory.Count];
        if(inventory.Count > 0) {
            for(int i = 0; i < saveData.items.Length; i++) {
                saveData.items[i] = inventory[i].id;
            }
        }

        // serialize
        bf.Serialize(file, saveData);
        file.Close();

        Debug.Log("FILESAVED!");
    }

    public static void LoadFile(string fName)
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
            SaveData = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("No save file to load!");
        }

        Flags.LoadData(SaveData.gameFlags);
        Inventory = ItemDatabase.instance.GetItemsFromIDs(SaveData.items);
        SceneManager.LoadSceneAsync(SaveData.currentSceneName);
    }

    void LoadJob() {
        if(currentJob != null) {
            if(saveData == null) {
                saveData = new SaveData();
            }

            foreach(JobData jobData in saveData.jobs) {
                if(jobData.id == currentJob.id) {
                    currentJob.LoadData(jobData);
                }
            }

            if(activeItem != null) {
                currentJob.player.carriedSprite = activeItem.worldSprite;
            }            
        }
    }

    void ConstructJobs() {
        saveData.jobs = new JobData[1];
        saveData.jobs[0] = new JobData { id = "POST_OFFICE", sceneName = "Job_PostOffice", name = "Post Office", isUnlocked = true };
    }

    #endregion

    #region Database Registration

    public static void RegisterJob(Job job)
    {
        Job = job;
        MessageEventManager.RegisterJob();
        GameManager.instance.LoadJob();
    }

    public static void RegisterPlayer(PlayerController player)
    {
        Job.player = player;
    }

    public static void RegisterNPC(NPC npc)
    {
        Job.npcs.Add(npc);
    }

    public static void RegisterMinigame(Minigame minigame)
    {
        Job.minigames.Add(minigame);
    }

    public static void RegisterContainer(Container container)
    {
        Job.containers.Add(container);
    }

    #endregion

    #region Events

    public delegate void TriggerEvent();
    public static event TriggerEvent OnResume;
    public static void Resume() {
        GameManager.instance.gameState = GameState.Default;

        if(OnResume != null) {
            OnResume();
        }
    }

    public delegate void PauseEvent(bool pausePlayer, bool pauseNPCs, params NPC[] exceptions);

    public static event PauseEvent OnPause;
    public static void Pause(bool pausePlayer, bool pauseNPCs, params NPC[] exceptions)
    {
        if(GameManager.instance.gameState == GameState.Default) {
            GameManager.instance.gameState = GameState.DefaultPause;
        }
        
        if (OnPause != null)
        {
            OnPause(pausePlayer, pauseNPCs, exceptions);
        }
    }

    public static void Pause(bool pausePlayer, bool pauseNPCs)
    {
        Pause(pausePlayer, pauseNPCs, new NPC[0]);
    }

    public delegate void IntEvent(int arg);

    public static event IntEvent OnProgressTime;
    public static void ProgressTime() {
        TimeChunk++;

        if(OnProgressTime != null) {
            OnProgressTime(TimeChunk);
        }
    }

    public static event IntEvent OnProgressDay;
    public static void ProgressDay() {
        Day++;
        TimeChunk = 0;

        if(OnProgressDay != null) {
            OnProgressDay(Day);
        }
    }

    public static event TriggerEvent OnJobExit;
    static void JobExit() {
        if(OnJobExit != null) {
            OnJobExit();
        }
    }

    #endregion

    #region Game Management

    public static void StartNewGame()
    {
        instance.filename = "Conner";
        SaveData = new SaveData();
        instance.ConstructJobs();
        SceneManager.LoadSceneAsync(instance.newGameScene);
    }

    public static void MovePlayerToSpawn()
    {
        Player.transform.position = Job.playerSpawn.position;
    }

    public static void LeaveJob(bool isSaving)
    {
        JobExit();
        MovePlayerToSpawn();
        
        if (isSaving)
        {
            instance.SaveFile();
        }

        SceneManager.LoadSceneAsync(instance.mapScene);
    }

    public static void QuitGame(bool isSaving)
    {
        MovePlayerToSpawn();
        if (isSaving)
        {
            instance.SaveFile();
        }

        SceneManager.LoadSceneAsync(instance.mainMenuScene);

        SaveData = null;
        Inventory.Clear();
        Job = null;
        instance.filename = null;
        Flags.Clear();
    }

    #endregion

    #region Inventory Management

    public static void SetActiveItem(Item item)
    {
        GameManager.ActiveItem = item;
        if(Job != null && Player != null)
        {
            Player.carriedSprite = item.worldSprite;
        }
    }

    public static void ClearActiveItem()
    {
        ActiveItem = null;
        if (Job != null && Player != null)
        {
            Player.carriedSprite = null;
        }
    }

    public static void AddItem(Item item) {
        Inventory.Add(item);
        SetActiveItem(item);
    }

    public static void RemoveItem(string id) {
        if(Inventory.Exists(x => x.id == id)) {
            Item item = Inventory.Find(x => x.id == id);
            if(item.id == ActiveItem.id)
            {
                ClearActiveItem();
            }
            Inventory.Remove(item);
        }
    }

    #endregion

}

public enum GameState { Default, DefaultPause, Menu, Dialogue, PauseMenu, Minigame };

[Serializable]
public class SaveData
{
    public string currentSceneName;
    public FlagData gameFlags;
    public JobData[] jobs;
    public string[] items;

    public SaveData() {
        gameFlags = new FlagData();
        jobs = new JobData[0];
        items = new string[0];
    }
}