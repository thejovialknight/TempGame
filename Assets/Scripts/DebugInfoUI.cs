using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfoUI : MonoBehaviour
{
    Text text;

    public static string interactInfo;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "Info: " + interactInfo;
    }
}
