﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable
{
    public string id = "WORLD_INSTANCE_ID";
    public Item item;

    public virtual void Awake() {
        GetComponent<SpriteRenderer>().sprite = item.worldSprite;
    }

    public virtual void InteractWith(Transform interactor) {
        GameManager.AddItem(item);
        SetCollectedFlag(true);
        GameObject.Destroy(gameObject);
    }

    public string GetInteractName() {
        return item.title;
    }
    public string GetInteractInfo() {
        return "Press E to Pickup";
    }

    public void SetCollectedFlag(bool isOn) {
        GameManager.JobFlags.SetFlag(id + "_COLLECTED", isOn);
    }

    public bool CheckCollectedFlag() {
        return GameManager.JobFlags.CheckFlag(id + "_COLLECTED");
    }

    public void DeleteIfAlreadyCollected() {
        if(CheckCollectedFlag()) {
            GameObject.Destroy(gameObject);
        }
    }
}
