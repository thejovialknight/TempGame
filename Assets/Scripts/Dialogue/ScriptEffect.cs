using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptEffect
{
    public EffectLabel effect;
    public string[] arguments;
    public ScriptCondition[] conditions;
}

public enum EffectLabel {
    ExitGame,
    ExitJob,
    CloseDialogue
}