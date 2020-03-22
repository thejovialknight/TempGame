using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageEventManager : MonoBehaviour
{
    public delegate void MessageEvent(string id);

    public static event MessageEvent OnReceiveMessageEvent;
    public static void RaiseOnReceiveMessage(string id)
    {
        if (OnReceiveMessageEvent != null)
        {
            OnReceiveMessageEvent(id);
        }
    }
}
