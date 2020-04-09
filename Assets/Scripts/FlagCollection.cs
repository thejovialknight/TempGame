using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagCollection
{
    public List<string> flags = new List<string>();

    public void SetFlag(string id, bool isOn)
    {
        bool flagExists = false;
        for (int i = flags.Count - 1; i >= 0; i--)
        {
            if (flags[i] == id)
            {
                flagExists = true;
                if (!isOn)
                {
                    flags.RemoveAt(i);
                }
            }
        }

        if(!flagExists)
        {
            if (isOn)
            {
                flags.Add(id);
            }
        }
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
}
