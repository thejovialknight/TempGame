using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : MonoBehaviour
{
    public string id;
    public string title;
    public string sceneName;
    public int currentDay;
    public int currentTimeChunk;
    public List<string> timeChunks = new List<string>();

    public FlagCollection flagCollection;

    void OnEnable()
    {
        MessageEventManager.OnReceiveMessageEvent += OnReceiveMessage;
    }

    void OnDisable()
    {
        MessageEventManager.OnReceiveMessageEvent -= OnReceiveMessage;
    }
    
    void Awake() {
        flagCollection = GetComponent<FlagCollection>();
    }

    void Start()
    {
        GameManager.manager.RegisterJob(this);
        GameManager.manager.isLoadingJob = true;
    }

    void OnReceiveMessage(string message) {
        if(message == "PROGRESS_TIME") {
            ProgressTime();
        }

        if(message == "PROGRESS_DAY") {
            ProgressDay();
        }
    }

    void ProgressTime() {
        currentTimeChunk++;
    }

    void ProgressDay() {
        currentDay++;
    }

    public bool CheckComplete() {
        return false;
    }
}
