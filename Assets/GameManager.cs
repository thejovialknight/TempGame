using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static string filename = "nullfile";
    public static bool isLoading = true;

    public static GameManager manager;

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
            Save();
        }

        if (Input.GetButtonDown("Quickload"))
        {
            Quickload();
        }

        if(isLoading) {
            Load(filename);
            isLoading = false;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        int filenameCount = 0;
        while(File.Exists(Path.Combine(Application.persistentDataPath, "saves", "tempsave" + filenameCount + ".dat"))) {
            filenameCount++;
        }
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "saves", "tempsave" + filenameCount + ".dat"));
        Debug.Log("File created!");

        // construct data
        saveData = new SaveData();
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

    public void Quickload() {
        int filenameCount = 0;
        while(File.Exists(Path.Combine(Application.persistentDataPath, "saves", "tempsave" + filenameCount + ".dat"))) {
            filenameCount++;
        }
        filenameCount--;
        Load("tempsave" + filenameCount + ".dat");
    }

    public void Load(string fName)
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
class JobData
{
    public NPCData[] npcs;
    public MinigameData[] minigames;
    public int day;
    public int time;
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