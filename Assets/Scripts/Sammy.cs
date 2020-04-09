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

    public override void OnDialogue(string id, string message)
    {
        base.OnDialogue(id, message);

        if(id != this.id) {
            return;
        }

        if(message == "OPEN") {
            mailSortObject.SetActive(true);
            MessageEventManager.MinigameStart("MAIL_SORT");
        }
    }

    public override void OnCutscene(string message) {
        base.OnCutscene(message);
    }
}
