using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sammy : NPC
{
    public GameObject mailSortObject;

    public Sammy()
    {
        id = "SAMMY";
        title = "Sammy";
        jobTitle = "Warehouse";
    }

    public override void OnReceiveMessage(string message)
    {
        if(CheckMessage(message, "OPEN")) {
            mailSortObject.SetActive(true);
            MessageEventManager.Broadcast("MAIL_SORT");
        }
    }
}
