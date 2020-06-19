using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueResponse
{
    public EvaluatedStringCollection text;
    public DialogueNode nextNode;
    public ScriptEffect[] effects;
    public ScriptCondition[] conditions;
}
