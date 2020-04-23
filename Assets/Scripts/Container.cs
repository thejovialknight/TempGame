using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IInteractable
{
    public string id = "NEW_CONTAINER_ID";
    public string title = "Container";
    public List<Item> inventory = new List<Item>();
    public Item itemToTake;

    void OnEnable()
    {
        MessageEventManager.OnDialogue += OnDialogue;
    }

    void OnDisable()
    {
        MessageEventManager.OnDialogue -= OnDialogue;
    }

    void OnDialogue(string id, string message, params string[] args) {
        if(this.id != id) {
            return;
        }

        if(message == "OPEN") {
            DialogueManager.instance.Say(title);
            ListOptions();
        }

        if(message == "TAKE")
        {
            string itemIDToTake = DialogueManager.CheckArg(args, 0);
            if (inventory.Exists(x => x.id == itemIDToTake))
            {
                itemToTake = inventory.Find(x => x.id == itemIDToTake);
                GameManager.instance.AddItem(itemToTake);
                inventory.Remove(itemToTake);
            }

            MessageEventManager.Dialogue(id, "TAKEN");
        }

        if(message == "TAKEN") {
            DialogueManager.instance.Say(itemToTake.title + " taken!");
            ListOptions();
        }

        if(message == "END") {
            DialogueManager.instance.Close();
        }
    }

    void ListOptions() {
        foreach(Item item in inventory) {
            DialogueManager.instance.AddOption(new DialogueOption("TAKE", id, "Take " + item.title, item.id));
        }

        DialogueManager.instance.AddOption(new DialogueOption("END", id, "Close Container"));
    }

    public void InteractWith(Transform interactor) {
        MessageEventManager.Dialogue(id, "OPEN");
    }

    public string GetInteractName() {
        return title;
    }

    public string GetInteractInfo() {
        return "Press SPACE to open";
    }
}
