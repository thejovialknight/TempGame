using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sammy : NPC
{
    public GameObject mailSortObject;

    public override void OnReceiveMessage(string id)
    {
        //mailSortObject.SetActive(true);
        //MessageEventManager.RaiseOnReceiveMessage("MAIL_SORT");
    }
}
