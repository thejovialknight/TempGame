using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DialogueNode", menuName = "ScriptableObjects/Dialogue Node", order = 1)]
public class DialogueNode : ScriptableObject
{
    public string npcName;
    public DialogueMessage[] messages;
    public DialogueResponse[] responses;
    public ScriptEffect[] effects;
}
