using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovement : MonoBehaviour
{
    float currentSpeed;
    float movementSpeed;

    public float baseSpeed;

    [Header("Component References")]
    public Rigidbody2D body;
    public Animator animator;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentSpeed = baseSpeed;
    }

    public void Move(Vector2 movementDirection)
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
}
