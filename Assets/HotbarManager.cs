using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public int currentItem;
    public Sprite emptySlotSprite;
    public List<Image> slotImages = new List<Image>();
    public GameObject hotbarObj;
    public bool isPaused = false;

    void OnEnable() {
        MessageEventManager.OnSetActiveItem += ValidateSlots;
        MessageEventManager.OnPause += OnPause;
        MessageEventManager.OnResume += OnResume;
    }

    void OnDisable() {
        MessageEventManager.OnSetActiveItem -= ValidateSlots;
        MessageEventManager.OnPause -= OnPause;
        MessageEventManager.OnResume += OnResume;
    }

    void Update() {
        if(!isPaused) {
            ValidateSlots(null);
        }
    }

    public void ValidateSlots(Item activeItem) {
        foreach(Image image in slotImages) {
            image.sprite = emptySlotSprite;
            image.color = Color.white;
        }
        
        List<Item> inventoryItems = GameManager.instance.items;
        for(int i = 0; i < inventoryItems.Count; i++) {
            if(slotImages.Count > i) {
                slotImages[i].sprite = inventoryItems[i].icon;
                if(inventoryItems[i].id == GameManager.instance.activeItem) {
                    slotImages[i].color = Color.white;
                }
                else {
                    slotImages[i].color = Color.grey;
                }
            }
        }
    }

    public void OnPause(bool pausePlayer, bool pauseNPCs, params NPC[] exceptions) {
        hotbarObj.SetActive(false);
        isPaused = true;
    }

    public void OnResume() {
        hotbarObj.SetActive(true);
        isPaused = false;
    }
}
