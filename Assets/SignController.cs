using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour, IInteractable
{
    public string signText = "Sign text here.";
    public string signName = "Sign";

    public void InteractWith(Transform interactor)
    {
        StartCoroutine("SetDialogue");
    }

    public string GetInteractInfo()
    {
        return signName;
    }

    public IEnumerator SetDialogue()
    {
        DebugDialogueUI.dialogue = signText;

        float cooldown = 2.0f;
        float currentCooldown = 0.0f;
        while (currentCooldown < cooldown)
        {
            currentCooldown += Time.deltaTime;
            yield return null;
        }

        DebugDialogueUI.dialogue = "";
    }
}
