using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour, IInteractable
{
    public string signText = "Sign text here.";
    public string signName = "Sign";

    public void InteractWith(Transform interactor)
    {
        
    }

    public string GetInteractName()
    {
        return signName;
    }

    public string GetInteractInfo()
    {
        return signText;
    }
}
