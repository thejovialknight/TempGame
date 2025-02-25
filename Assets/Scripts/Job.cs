﻿using System.Collections;
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
    public List<Quest> quests = new List<Quest>();
    public List<Container> containers = new List<Container>();

    public Transform playerSpawn;

    void Awake() {
        GameManager.RegisterJob(this);
    }

    public void LoadData(JobData dataToLoad) {
        title = dataToLoad.name;
        sceneName = dataToLoad.sceneName;
        isUnlocked = dataToLoad.isUnlocked;
        currentDay = dataToLoad.day;
        currentTimeChunk = dataToLoad.time;

        flagCollection.LoadData(dataToLoad.flags);

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

            foreach (Container container in containers)
            {
                foreach (ContainerData containerData in dataToLoad.containers)
                {
                    if (container.id == containerData.id)
                    {
                        container.LoadData(containerData);
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
        dataToSave.flags = flagCollection.SaveData();

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

        dataToSave.quests = new QuestData[quests.Count];
        for(int i = 0; i < dataToSave.quests.Length; i++) {
            dataToSave.quests[i] = quests[i].SaveData();
        }

        dataToSave.containers = new ContainerData[containers.Count];
        for (int i = 0; i < dataToSave.containers.Length; i++)
        {
            dataToSave.containers[i] = containers[i].SaveData();
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
    public FlagData flags;
    public PlayerData player;
    public NPCData[] npcs;
    public MinigameData[] minigames;
    public QuestData[] quests;
    public ContainerData[] containers;
}
