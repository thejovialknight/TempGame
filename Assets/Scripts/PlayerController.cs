using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isPaused= false;

    public float baseSpeed;
    public float currentSpeed;

    public float movementSpeed;
    public Rigidbody2D body;
    public Animator animator;
    public AudioSource audioSource;
    public Collider2D col;

    public Vector2 interactOffset;
    public float interactRange = 2.0f;
    Collider2D[] interactColliders;

    void OnEnable()
    {
        MessageEventManager.OnPause += OnPause;
        MessageEventManager.OnResume += OnResume;
        MessageEventManager.OnJobRegister += OnJobRegister;
    }

    void OnDisable()
    {
        MessageEventManager.OnPause -= OnPause;
        MessageEventManager.OnResume -= OnResume;
        MessageEventManager.OnJobRegister -= OnJobRegister;
    }

    void OnPause(bool pausePlayer, bool pauseNPCs, params NPC[] exceptions)
    {
        if (pausePlayer)
        {
            isPaused = true;
            Move(new Vector2(0.0f, 0.0f));
        }
    }

    void OnResume()
    {
        isPaused = false;
    }

    public void OnJobRegister() {
        GameManager.manager.RegisterPlayer(this);
    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();

        currentSpeed = baseSpeed;
    }

    void Start() {
        GetInteractColliders();
    }

    void Update()
    {
        if (!isPaused)
        {
            Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
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

    void Move(Vector2 movementDirection)
    {
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
        body.velocity = movementDirection * movementSpeed * currentSpeed;
        animator.SetFloat("Velocity", movementSpeed);
        if(movementDirection.x < 0.0f)
        {
            animator.SetFloat("xDirection", -1.0f);
        }
        if (movementDirection.x > 0.0f)
        {
            animator.SetFloat("xDirection", 1.0f);
        }
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
            if(collider.GetComponent<IInteractable>() != null) {
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