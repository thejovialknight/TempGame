using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueOption
{
    public string id;
    public string msg;

    public DialogueOption(string id, string msg)
    {
        this.id = id;
        this.msg = msg;
    }
}
