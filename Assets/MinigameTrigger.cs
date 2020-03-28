using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour, IInteractable
{
    public string interactName = "Minigame";
    public GameObject minigameObject;

    public void InteractWith(Transform interactor) {
        minigameObject.SetActive(true);
    }
    public string GetInteractName() {
        return interactName;
    }
    public string GetInteractInfo() {
        return "Press E to Play";
    }
}
