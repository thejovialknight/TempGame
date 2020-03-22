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

    public Vector2 interactOffset;
    public float interactRange = 2.0f;
    Collider2D[] interactColliders;

    void OnEnable()
    {
        MessageEventManager.OnPauseEvent += OnPause;
        MessageEventManager.OnResumeEvent += OnResume;
    }

    void OnDisable()
    {
        MessageEventManager.OnPauseEvent -= OnPause;
        MessageEventManager.OnResumeEvent -= OnResume;
    }

    void OnPause()
    {
        isPaused = true;
        Move(new Vector2(0.0f, 0.0f));
    }

    void OnResume()
    {
        isPaused = false;
    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        currentSpeed = baseSpeed;
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
        foreach(Collider2D collider in interactColliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if(interactable != null)
            {
                MessageEventManager.RaiseOnSetInteractInfo(interactable.GetInteractName(), interactable.GetInteractInfo());
                if (Input.GetButtonDown("Interact"))
                {
                    interactable.InteractWith(transform);
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
