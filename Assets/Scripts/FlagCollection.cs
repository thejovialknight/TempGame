using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagCollection
{
    public List<string> flags = new List<string>();
    public List<IntFlag> intFlags = new List<IntFlag>();
    public List<FloatFlag> floatFlags = new List<FloatFlag>();
    public List<StringFlag> stringFlags = new List<StringFlag>();

    public void Clear() {
        flags.Clear();
    }

    public void SetFlag(string id, bool isOn)
    {
        for (int i = flags.Count - 1; i >= 0; i--)
        {
            if (flags[i] == id)
            {
                if (!isOn)
                {
                    flags.RemoveAt(i);
                }
                return;
            }
        }

        if (isOn)
        {
            flags.Add(id);
        }
    }

    public void SetIntFlag(string id, int value)
    {
        for (int i = intFlags.Count - 1; i >= 0; i--)
        {
            if (id == intFlags[i].id)
            {
                intFlags[i] = new IntFlag(id, value);
                return;
            }
        }

        intFlags.Add(new IntFlag(id, value));
    }

    public void SetFloatFlag(string id, float value)
    {
        for (int i = floatFlags.Count - 1; i >= 0; i--)
        {
            if (id == floatFlags[i].id)
            {
                floatFlags[i] = new FloatFlag(id, value);
                return;
            }
        }

        floatFlags.Add(new FloatFlag(id, value));
    }

    public void SetStringFlag(string id, string value)
    {
        for (int i = stringFlags.Count - 1; i >= 0; i--)
        {
            if (id == stringFlags[i].id)
            {
                stringFlags[i] = new StringFlag(id, value);
                return;
            }
        }

        stringFlags.Add(new StringFlag(id, value));
    }

    public bool CheckFlag(string id)
    {
        bool flagExists = false;
        foreach(string flag in flags)
        {
            if(id == flag)
            {
                flagExists = true;
            }
        }
        return flagExists;
    }

    public int CheckIntFlag(string id)
    {
        foreach(IntFlag flag in intFlags)
        {
            if(id == flag.id)
            {
                return flag.value;
            }
        }

        return 0;
    }

    public float CheckFloatFlag(string id)
    {
        foreach(FloatFlag flag in floatFlags)
        {
            if(id == flag.id)
            {
                return flag.value;
            }
        }
        
        return 0.0f;
    }

    public string CheckStringFlag(string id)
    {
        foreach(StringFlag flag in stringFlags)
        {
            if(id == flag.id)
            {
                return flag.value;
            }
        }
        
        return null;
    }

    public FlagData SaveData() {
        FlagData dataToSave = new FlagData();
        dataToSave.flags = flags.ToArray();

        // save ints
        List<string> intFlagIDList = new List<string>();
        List<int> intFlagValueList = new List<int>();
        foreach(IntFlag flag in intFlags) {
            intFlagIDList.Add(flag.id);
            intFlagValueList.Add(flag.value);
        }
        dataToSave.intFlagIDs = intFlagIDList.ToArray();
        dataToSave.intFlagValues = intFlagValueList.ToArray();

        // save floats
        List<string> floatFlagIDList = new List<string>();
        List<float> floatFlagValueList = new List<float>();
        foreach(FloatFlag flag in floatFlags) {
            floatFlagIDList.Add(flag.id);
            floatFlagValueList.Add(flag.value);
        }
        dataToSave.floatFlagIDs = floatFlagIDList.ToArray();
        dataToSave.floatFlagValues = floatFlagValueList.ToArray();

        // save strings
        List<string> stringFlagIDList = new List<string>();
        List<string> stringFlagValueList = new List<string>();
        foreach(StringFlag flag in stringFlags) {
            stringFlagIDList.Add(flag.id);
            stringFlagValueList.Add(flag.value);
        }
        dataToSave.stringFlagIDs = stringFlagIDList.ToArray();
        dataToSave.stringFlagValues = stringFlagValueList.ToArray();

        return dataToSave;
    }

    public void LoadData(FlagData dataToLoad) {
        if (dataToLoad != null)
        {
            if (dataToLoad.flags != null)
            {
                flags = new List<string>(dataToLoad.flags);
            }

            // load ints
            if (dataToLoad.intFlagIDs != null)
            {
                intFlags = new List<IntFlag>();
                for (int i = 0; i < dataToLoad.intFlagIDs.Length; i++)
                {
                    intFlags.Add(new IntFlag(dataToLoad.intFlagIDs[i], dataToLoad.intFlagValues[i]));
                }
            }

            // load floats
            if (dataToLoad.floatFlagIDs != null)
            {
                floatFlags = new List<FloatFlag>();
                for (int i = 0; i < dataToLoad.floatFlagIDs.Length; i++)
                {
                    floatFlags.Add(new FloatFlag(dataToLoad.floatFlagIDs[i], dataToLoad.floatFlagValues[i]));
                }
            }

            // load strings
            if (dataToLoad.stringFlagIDs != null)
            {
                stringFlags = new List<StringFlag>();
                for (int i = 0; i < dataToLoad.stringFlagIDs.Length; i++)
                {
                    stringFlags.Add(new StringFlag(dataToLoad.stringFlagIDs[i], dataToLoad.stringFlagValues[i]));
                }
            }
        }
    }
}

[System.Serializable]
public class FlagData {
    public string[] flags;
    public string[] intFlagIDs;
    public string[] floatFlagIDs;
    public string[] stringFlagIDs;
    public int[] intFlagValues;
    public float[] floatFlagValues;
    public string[] stringFlagValues;
}