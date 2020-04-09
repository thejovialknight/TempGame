using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arlene : NPC
{
    public Arlene()
    {
        id = "ARLENE";
        title = "Arlene";
        jobTitle = "General Manager";
    }

    public override void OnReceiveMessage(string message)
    {
        base.OnReceiveMessage(message);
    }
}
