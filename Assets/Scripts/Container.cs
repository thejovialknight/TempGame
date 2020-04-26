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
        MessageEventManager.OnJobRegister += OnJobRegister;
        MessageEventManager.OnDialogue += OnDialogue;
    }

    void OnDisable()
    {
        MessageEventManager.OnJobRegister += OnJobRegister;
        MessageEventManager.OnDialogue -= OnDialogue;
    }

    void OnJobRegister()
    {
        GameManager.RegisterContainer(this);
    }

    void OnDialogue(string id, string message, params string[] args) {
        if(this.id != id) {
            return;
        }

        if(message == "OPEN") {
            DialogueManager.Say(title);
            ListOptions();
        }

        if(message == "TAKE")
        {
            string itemIDToTake = DialogueManager.CheckArg(args, 0);
            if (inventory.Exists(x => x.id == itemIDToTake))
            {
                itemToTake = inventory.Find(x => x.id == itemIDToTake);
                GameManager.AddItem(itemToTake);
                inventory.Remove(itemToTake);
            }

            MessageEventManager.Dialogue(id, "TAKEN");
        }

        if(message == "TAKEN") {
            DialogueManager.Say(itemToTake.title + " taken!");
            ListOptions();
        }

        if(message == "END") {
            DialogueManager.Close();
        }
    }

    void ListOptions() {
        foreach(Item item in inventory) {
            DialogueManager.AddOption(new DialogueOption("TAKE", id, "Take " + item.title, item.id));
        }

        DialogueManager.AddOption(new DialogueOption("END", id, "Close Container"));
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

    public void LoadData(ContainerData dataToLoad)
    {
        id = dataToLoad.id;

        inventory.Clear();
        foreach(string itemID in dataToLoad.items)
        {
            if (ItemDatabase.instance.GetItemFromID(itemID) != null)
            {
                inventory.Add(ItemDatabase.instance.GetItemFromID(itemID));
            }
        }
    }

    public ContainerData SaveData()
    {
        ContainerData dataToSave = new ContainerData();
        dataToSave.id = id;
        List<string> itemIDs = new List<string>();
        foreach(Item item in inventory)
        {
            itemIDs.Add(item.id);
        }
        dataToSave.items = itemIDs.ToArray();

        return dataToSave;
    }
}

[System.Serializable]
public class ContainerData
{
    public string id = "NEW_CONTAINER_ID";
    public string[] items;
}