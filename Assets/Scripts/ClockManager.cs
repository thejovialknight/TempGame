using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockManager : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;

    void Update()
    {
        Job job = GameManager.manager.currentJob;
        dayText.text = "Day " + job.currentDay;
        timeText.text = job.timeChunks[job.currentTimeChunk];
    }
}
