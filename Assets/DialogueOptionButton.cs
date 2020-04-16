using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueOptionButton : MonoBehaviour
{
    public DialogueOption option;

    public TextMeshProUGUI buttonText;

    public void Init(DialogueOption option)
    {
        this.option = option;
        buttonText.text = option.message;
    }

    public delegate void DialogueOptionEvent(DialogueOption dialogueOption);
    public static event DialogueOptionEvent OnDialoguePressed;
    public void RaiseOnDialoguePressed()
    {
        if (OnDialoguePressed != null)
        {
            OnDialoguePressed(option);
        }
    }
}
