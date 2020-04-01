using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public GameObject playerObject;
    public List<NPC> NPCs = new List<NPC>();
    public List<Minigame> minigames = new List<Minigame>();

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
            Save();
        }

        if (Input.GetButtonDown("Quickload"))
        {
            Load();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "saves", "tempsave.dat"));
        Debug.Log("File created!");

        // construct data
        SaveData saveData = new SaveData();
        Debug.Log("saveData created!");

        // construct flags
        saveData.gameFlags = flagCollection.flags.ToArray();
        Debug.Log("Flags constructed!");

        // construct PlayerData
        PlayerData playerData = new PlayerData();
        playerData.position = new float[2] { playerObject.transform.position.x, playerObject.transform.position.y };
        saveData.playerData = playerData;
        Debug.Log("Player constructed!");

        // construct NPCDatas
        List<NPCData> NPCDatas = new List<NPCData>();
        foreach(NPC npc in NPCs)
        {
            NPCData data = new NPCData();

            data.id = npc.id;
            data.position = new float[2] { npc.transform.position.x, npc.transform.position.y };
            data.flags = npc.flagCollection.flags.ToArray();
            NPCDatas.Add(data);
            Debug.Log("NPC Added!");
        }
        saveData.NPCData = NPCDatas.ToArray();
        Debug.Log("NPCs constructed!");

        // construct MinigameDatas
        List<MinigameData> minigameDatas = new List<MinigameData>();
        foreach (Minigame minigame in minigames)
        {
            MinigameData data = new MinigameData();
            data.id = minigame.id;
            data.rating = minigame.bestRating;
            data.score = minigame.bestScore;
            minigameDatas.Add(data);
            Debug.Log("Minigame Added!");
        }
        saveData.minigameData = minigameDatas.ToArray();
        Debug.Log("Minigames constructed!");

        // serialize
        bf.Serialize(file, saveData);
        Debug.Log("Serialized!");

        file.Close();
        Debug.Log("Closed!");

    }

    public void Load()
    {
        if(File.Exists(Path.Combine(Application.persistentDataPath, "tempsave.dat")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "tempsave.dat"), FileMode.Open);

            //deserialize
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();

            // construct data
            // load game flags
            flagCollection.flags = new List<string>(saveData.gameFlags);

            // load PlayerData
            playerObject.transform.position = new Vector3(saveData.playerData.position[0], saveData.playerData.position[1], 0f);

            // load NPCDatas
            foreach (NPC npc in NPCs)
            {
                foreach(NPCData data in saveData.NPCData)
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
                foreach (MinigameData data in saveData.minigameData)
                {
                    if (data.id == minigame.id)
                    {
                        minigame.bestRating = data.rating;
                        minigame.bestScore = data.score;
                    }
                }
            }
        }
        else
        {
            Debug.Log("No save file to load!");
        }
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
    public string[] gameFlags;
    public PlayerData playerData;
    public NPCData[] NPCData;
    public MinigameData[] minigameData;
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