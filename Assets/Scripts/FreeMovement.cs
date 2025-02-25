﻿using System.Collections;
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
        Move(movementDirection, currentSpeed);
    }

    public void Move(Vector2 movementDirection, float speed)
    {
        movementDirection = Vector2.ClampMagnitude(movementDirection, 1f);
        body.velocity = movementDirection * speed;

        animator.SetFloat("Velocity", body.velocity.magnitude);
        if (movementDirection.x < 0.0f)
        {
            animator.SetFloat("xDirection", -1.0f);
        }
        if (movementDirection.x > 0.0f)
        {
            animator.SetFloat("xDirection", 1.0f);
        }
        if(animator.GetFloat("Velocity") > 0.1f) {
            animator.speed = body.velocity.magnitude / 4f;
        }
    }
}
