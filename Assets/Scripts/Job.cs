using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Job : MonoBehaviour
{
    public string id;
    public string title;
    public string sceneName;
    public int currentDay;
    public int currentTimeChunk;
    public List<string> timeChunks = new List<string>();
    public FlagCollection flagCollection;
    public List<NPC> NPCs = new List<NPC>();
    public List<Minigame> minigames = new List<Minigame>();
}
