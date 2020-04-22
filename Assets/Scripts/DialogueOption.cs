using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueOption
{
    public string nodeID;
    public string receiverID;
    public string message;
    public string[] args;

    public DialogueOption(string nodeID, string receiverID, string message, params string[] args)
    {
        this.nodeID = nodeID;
        this.receiverID = receiverID;
        this.message = message;
        this.args = args;
    }

    public DialogueOption(string nodeID, string receiverID, string message)
    {
        this.nodeID = nodeID;
        this.receiverID = receiverID;
        this.message = message;
        this.args = null;
    }
}
