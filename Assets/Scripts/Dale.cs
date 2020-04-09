using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dale : NPC
{
    public Dale()
    {
        id = "DALE";
        title = "Dale";
        jobTitle = "Front Counter";
    }

    public override void OnReceiveMessage(string message)
    {
        base.OnReceiveMessage(message);
    }
}
