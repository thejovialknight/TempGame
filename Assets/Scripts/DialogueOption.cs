using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueOption
{
    public string optionID;
    public string receiverID;
    public string message;

    public DialogueOption(string optionID, string receiverID, string message)
    {
        this.optionID = optionID;
        this.receiverID = receiverID;
        this.message = message;
    }
}
