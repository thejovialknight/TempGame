using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptStringEvaluation
{
    public EvaluationLabel evaluation;
    public string arguments;
}

public enum EvaluationLabel {
    Text,
    PlayerName,
    NPCStringFlag
}