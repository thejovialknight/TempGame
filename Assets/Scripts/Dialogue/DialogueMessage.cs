using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueMessage
{
    public EvaluatedStringCollection[] text;
    public ScriptCondition[] conditions;
}