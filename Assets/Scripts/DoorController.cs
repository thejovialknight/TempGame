using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    float currentCloseCooldown;

    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    public Sprite closedSprite;
    public Sprite openSprite;
    public AudioClip openSound;
    public AudioClip closeSound;
    public float automaticCloseCooldown = 1.5f;
    public bool isAutomaticallyClosed = true;
    public bool isOpen = false;
    public string doorName = "Door";

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start() {
        ValidateOpen();
    }

    void Update()
    {
        currentCloseCooldown -= Time.deltaTime;

        if(isAutomaticallyClosed && isOpen && currentCloseCooldown <= 0f)
        {
            Close();
        }
    }

    void OnValidate() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        ValidateOpen();
    }

    public void InteractWith(Transform interactor) {
        if(isOpen) {
            Close();
        }
        else 
        {
            Open();
        }
    }

    public string GetInteractName() {
        return doorName;
    }

    public string GetInteractInfo() {
        return "Press E to open";
    }

    void Open()
    {
        AudioSource.PlayClipAtPoint(openSound, transform.position);
        isOpen = true;
        ValidateOpen();
        currentCloseCooldown = automaticCloseCooldown;
    }

    void Close()
    {
        isOpen = false;
        AudioSource.PlayClipAtPoint(closeSound, transform.position);
        ValidateOpen();
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
