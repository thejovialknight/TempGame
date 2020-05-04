using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    string id;
    QuestState state;
    FlagCollection flagCollection;
    public string ID {
        get {
            return id;
        }
        set {
            id = value;
        }
    }
    public FlagCollection Flags {
        get {
            return flagCollection;
        }
        set {
            flagCollection = value;
        }
    }
    public QuestState State {
        get {
            return state;
        }
        set {
            state = value;
        }
    }

    public Quest(string id, QuestState state) {
        this.id = id;
        this.state = state;
    }

    public void LoadData(QuestData dataToLoad) {
        state = (QuestState)System.Enum.Parse(typeof(QuestState), dataToLoad.state );
        flagCollection.LoadData(dataToLoad.flags);
    }

    public QuestData SaveData() {
        QuestData dataToSave = new QuestData();
        dataToSave.id = id;
        dataToSave.state = state.ToString();
        dataToSave.flags = flagCollection.SaveData();
        return dataToSave;
    }

}

public enum QuestState {
    Inactive,
    Rejected,
    InProgress,
    Complete,
    Failed
}

public class QuestData {
    public string id;
    public string state;
    public FlagData flags;
}