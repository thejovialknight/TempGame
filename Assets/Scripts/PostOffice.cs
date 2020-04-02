using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostOffice : Job
{
    new public string id = "POST_OFFICE";
    new public string title = "Post Office";
    new public string sceneName = "Job_PostOffice";
    new public int currentDay = 1;
    new public int currentTimeChunk = 1;
    new public List<string> timeChunks = new List<string>() { "7:00 AM", "10:00 AM", "1:00 PM", "4:00 PM" };
}
