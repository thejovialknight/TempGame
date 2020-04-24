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
        movementDirection = Vector2.ClampMagnitude(movementDirection, 1f);
        body.velocity = movementDirection * currentSpeed;

        animator.SetFloat("Velocity", body.velocity.magnitude);
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
