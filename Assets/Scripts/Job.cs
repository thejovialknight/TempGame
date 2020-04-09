using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Job : MonoBehaviour
{
    public string id;
    public bool isUnlocked;
    public string title;
    public string sceneName;
    public int currentDay;
    public int currentTimeChunk;
    public List<string> timeChunks = new List<string>();
    public FlagCollection flagCollection = new FlagCollection();
    public PlayerController player;
    public List<NPC> npcs = new List<NPC>();
    public List<Minigame> minigames = new List<Minigame>();

    public Transform playerSpawn;

    void Awake() {
        GameManager.manager.RegisterJob(this);
    }

    void Start() {
        GameManager.manager.LoadJob();
    }

    public void LoadData(JobData dataToLoad) {
        title = dataToLoad.name;
        sceneName = dataToLoad.sceneName;
        isUnlocked = dataToLoad.isUnlocked;
        currentDay = dataToLoad.day;
        currentTimeChunk = dataToLoad.time;

        flagCollection.flags = new List<string>(dataToLoad.flags);

        if(dataToLoad.hasBeenSaved) {
            player.LoadData(dataToLoad.player);
            foreach(NPC npc in npcs) {
                foreach(NPCData npcData in dataToLoad.npcs) {
                    if(npc.id == npcData.id) {
                        npc.LoadData(npcData);
                    }
                }
            }

            foreach(Minigame minigame in minigames) {
                foreach(MinigameData minigameData in dataToLoad.minigames) {
                    if(minigame.id == minigameData.id) {
                        minigame.LoadData(minigameData);
                    }
                }
            }
        }
    }

    public JobData SaveData() {
        JobData dataToSave = new JobData();
        dataToSave.hasBeenSaved = true;
        dataToSave.id = id;
        dataToSave.sceneName = sceneName;
        dataToSave.name = title;
        dataToSave.isUnlocked = isUnlocked;
        dataToSave.day = currentDay;
        dataToSave.time = currentTimeChunk;
        dataToSave.flags = flagCollection.flags.ToArray();

        if(player != null) {
            dataToSave.player = player.SaveData();
        }

        dataToSave.npcs = new NPCData[npcs.Count];
        for(int i = 0; i < dataToSave.npcs.Length; i++) {
            dataToSave.npcs[i] = npcs[i].SaveData();
        }

        dataToSave.minigames = new MinigameData[minigames.Count];
        for(int i = 0; i < dataToSave.minigames.Length; i++) {
            dataToSave.minigames[i] = minigames[i].SaveData();
        }

        return dataToSave;
    }
}

[System.Serializable]
public class JobData
{
    public bool hasBeenSaved = false;
    public string id;
    public string sceneName;
    public string name;
    public bool isUnlocked;
    public int day;
    public int time;
    public string[] flags = new string[0];
    public PlayerData player;
    public NPCData[] npcs;
    public MinigameData[] minigames;
}
