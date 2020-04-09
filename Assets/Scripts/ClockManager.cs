using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    public Text dayText;
    public Text timeText;

    void Update()
    {
        Job job = GameManager.manager.currentJob;
        dayText.text = "Day " + job.currentDay;
        timeText.text = job.timeChunks[job.currentTimeChunk];
    }
}
