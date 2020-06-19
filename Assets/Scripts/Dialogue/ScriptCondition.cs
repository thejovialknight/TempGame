using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptCondition
{
    public ConditionLabel condition;
    public string[] arguments;
}

public enum ConditionLabel {
    FlagValue
}
