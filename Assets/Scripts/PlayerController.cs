using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isPaused = false;

    [Header("Component References")]
    public Rigidbody2D body;
    public Animator animator;
    public FreeMovement freeMovement;
    public AudioSource audioSource;
    public Collider2D col;

    [Header("External References")]
    public SpriteRenderer carriedSpriteRenderer;
    public Sprite carriedSprite;

    [Header("Interactor Configuration")]
    public Vector2 interactOffset;
    public float interactRange = 2.0f;
    Collider2D[] interactColliders;

    void OnEnable()
    {
        GameManager.OnPause += OnPause;
        GameManager.OnResume += OnResume;
        MessageEventManager.OnJobRegister += OnJobRegister;
    }

    void OnDisable()
    {
        GameManager.OnPause -= OnPause;
        GameManager.OnResume -= OnResume;
        MessageEventManager.OnJobRegister -= OnJobRegister;
    }

    void OnPause(bool pausePlayer, bool pauseNPCs, params NPC[] exceptions)
    {
        if (pausePlayer)
        {
            isPaused = true;
            freeMovement.Move(new Vector2(0.0f, 0.0f));
        }
    }

    void OnResume()
    {
        isPaused = false;
    }

    public void OnJobRegister() {
        GameManager.RegisterPlayer(this);
    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        freeMovement = GetComponent<FreeMovement>();
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
    }

    void Start() {
        GetInteractColliders();
    }

    void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (!isPaused)
        {
            if(Input.GetButton("Sprint")) {
                freeMovement.Move(movementVector, freeMovement.baseSpeed * 1.33f);
            }
            else {
                freeMovement.Move(movementVector);
            }
            Interact();
        }

        if(body.velocity.magnitude > 0.2f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        if(carriedSprite != null)
        {
            carriedSpriteRenderer.sprite = carriedSprite;
            animator.SetBool("Carrying Item", true);
        }
        else
        {
            carriedSpriteRenderer.sprite = null;
            animator.SetBool("Carrying Item", false);
        }
    }

    void FixedUpdate() {
        if (!isPaused)
        {
            GetInteractColliders();
        }
    }

    public void LoadData(PlayerData dataToLoad) {
        if(dataToLoad.hasBeenSaved) {
            transform.position = new Vector3(dataToLoad.position[0], dataToLoad.position[1], 0f);
        }
    }

    public PlayerData SaveData() {
        PlayerData dataToSave = new PlayerData();
        dataToSave.hasBeenSaved = true;
        dataToSave.position = new float[2] { transform.position.x, transform.position.y };
        return dataToSave;
    }

    void GetInteractColliders()
    {
        interactColliders = Physics2D.OverlapCircleAll(transform.position + (Vector3)interactOffset, interactRange);
    }

    void Interact()
    {
        MessageEventManager.RaiseOnClearInteractInfo();

        Collider2D interactCollider = null;
        foreach(Collider2D collider in interactColliders)
        {
            if(collider != null && collider.GetComponent<IInteractable>() != null) {
                if(interactCollider != null) {
                    if(collider.Distance(col).distance < interactCollider.Distance(col).distance) {
                        interactCollider = collider;
                    }
                }
                else {
                    interactCollider = collider;
                }
            }
        }

        if (interactCollider != null)
        {
            IInteractable interactable = interactCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                IInteractableHelper[] interactHelpers = interactCollider.GetComponents<IInteractableHelper>();
                foreach (IInteractableHelper interactHelper in interactHelpers)
                {
                    interactHelper.OnEnter();
                }

                MessageEventManager.RaiseOnSetInteractInfo(interactable.GetInteractName(), interactable.GetInteractInfo());
                if (Input.GetButtonDown("Interact"))
                {
                    interactable.InteractWith(transform);
                    foreach (IInteractableHelper interactHelper in interactHelpers)
                    {
                        interactHelper.OnInteract(transform);
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (Vector3)interactOffset, interactRange);
    }
}

[System.Serializable]
public class PlayerData
{
    public bool hasBeenSaved = false;
    public float[] position;
}