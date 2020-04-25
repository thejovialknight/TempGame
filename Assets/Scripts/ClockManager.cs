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
        dayText.text = "Day " + GameManager.Day;
        timeText.text = GameManager.TimeChunkString;
    }
}
