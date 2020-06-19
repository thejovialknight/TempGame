using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueOptionButton : MonoBehaviour
{
    public DialogueResponse response;

    public TextMeshProUGUI buttonText;

    public void Init(DialogueResponse response)
    {
        this.response = response;
        buttonText.text = response.text.Evaluate();
    }

    public delegate void DialogueOptionEvent(DialogueResponse dialogueResponse);
    public static event DialogueOptionEvent OnDialoguePressed;
    public void RaiseOnDialoguePressed(DialogueResponse dialogueResponse)
    {
        if (OnDialoguePressed != null)
        {
            OnDialoguePressed(dialogueResponse);
        }
    }
}
