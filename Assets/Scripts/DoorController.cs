using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    public Sprite closedSprite;
    public Sprite openSprite;
    public bool isOpen = false;
    public string doorName = "Door";

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start() {
        ValidateOpen();
    }

    void OnValidate() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        ValidateOpen();
    }

    public void InteractWith(Transform interactor) {
        if(isOpen) {
            isOpen = false;
        }
        else 
        {
            isOpen = true;
        }
        ValidateOpen();
    }
    public string GetInteractName() {
        return doorName;
    }
    public string GetInteractInfo() {
        return "Press E to open";
    }

    void ValidateOpen() {
        if(isOpen) {
            spriteRenderer.sprite = openSprite;
            boxCollider.offset = new Vector2(-0.25f, -0.8125f);
            boxCollider.size = new Vector2(0.125f, 0.75f);
        }
        else 
        {
            spriteRenderer.sprite = closedSprite;
            boxCollider.offset = new Vector2(0.0f, -0.5f);
            boxCollider.size = new Vector2(0.625f, 0.125f);
        }
    }
}
