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
        GameManager.OnPause += OnPause;
        GameManager.OnResume += OnResume;
    }

    void OnDisable() {
        MessageEventManager.OnSetActiveItem -= ValidateSlots;
        GameManager.OnPause -= OnPause;
        GameManager.OnResume += OnResume;
    }

    void Update() {
        if(!isPaused) {
            ValidateSlots(null);
        }

        int keyPressed = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            keyPressed = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            keyPressed = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            keyPressed = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            keyPressed = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            keyPressed = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            keyPressed = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            keyPressed = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            keyPressed = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            keyPressed = 9;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            keyPressed = 10;
        }

        if(keyPressed != -1) {
            if(GameManager.instance.items.Count > keyPressed - 1) {
                GameManager.instance.SetActiveItem(GameManager.instance.items[keyPressed - 1]);
            }
            else {
                GameManager.instance.ClearActiveItem();
            }
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
                if(GameManager.instance.GetActiveItem() != null && inventoryItems[i].id == GameManager.instance.GetActiveItem().id) {
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
        if(hotbarObj != null) {
            hotbarObj.SetActive(true);
        }
        isPaused = false;
    }
}
