using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed;
    public float currentSpeed;

    public float movementSpeed;
    public Rigidbody2D body;
    public Animator animator;
    public AudioSource audioSource;

    public Vector2 interactOffset;
    public float interactRange = 2.0f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        currentSpeed = baseSpeed;
    }

    void Update()
    {
        Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        Interact();

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

    void Interact()
    {
        DebugInfoUI.interactInfo = "";

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + (Vector3)interactOffset, interactRange);
        foreach(Collider2D collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if(interactable != null)
            {
                DebugInfoUI.interactInfo = interactable.GetInteractInfo();
                if(Input.GetButtonDown("Interact"))
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
