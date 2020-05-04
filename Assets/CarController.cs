using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float desiredDirection = 0f;
    public float currentDirection = 0f;
    public float currentSpeed = 0f;

    FreeMovement movement;

    public float acceleration;
    public float passiveDeceleration;
    public float turnDeceleration;
    public float turnSpeed;
    public float brakeSpeed;
    public float driftSpeed;

    [Header("References")]
    public AudioSource brakeSound;

    void Awake()
    {
        movement = GetComponent<FreeMovement>();
    }

    void Update()
    {
        currentDirection = Mathf.Lerp(currentDirection, desiredDirection, driftSpeed * Time.deltaTime);

        Vector2 facingDirection = new Vector2(Mathf.Cos(currentDirection), Mathf.Sin(currentDirection));

        if (currentSpeed != 0f) {
            currentDirection = Mathf.Lerp(currentDirection, desiredDirection, driftSpeed * Time.deltaTime);
            float decelerationFactor = passiveDeceleration;
            float currentTurnSpeed = 0f;

            bool isBraking = Input.GetButton("Brake");
            if (isBraking)
            {
                if (!brakeSound.isPlaying)
                {
                    brakeSound.Play();
                    brakeSound.pitch = Mathf.Clamp((currentSpeed / movement.baseSpeed) + 0.25f, 0.75f, 1.25f);
                }
                decelerationFactor += brakeSpeed;
            }
            brakeSound.volume = currentSpeed / movement.baseSpeed;

            if (Mathf.Abs(currentSpeed) > 0.2f)
            {
                currentTurnSpeed = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                decelerationFactor += (Mathf.Abs(currentTurnSpeed) * turnDeceleration) * currentSpeed;
                desiredDirection -= currentTurnSpeed;
            }
            else
            {
                desiredDirection = currentDirection;
            }

            if (currentSpeed > 0f)
            {
                currentSpeed -= decelerationFactor * Time.deltaTime;
            }
            else if (currentSpeed < 0f)
            {
                currentSpeed += decelerationFactor * Time.deltaTime;
            }
        }

        currentSpeed += acceleration * Input.GetAxisRaw("Vertical") * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -movement.baseSpeed * 0.5f, movement.baseSpeed);
        movement.Move(facingDirection, currentSpeed);

        GetComponent<Animator>().speed = Mathf.Abs(currentSpeed * 0.075f);
        GetComponent<AudioSource>().volume = Mathf.Clamp(Mathf.Abs(currentSpeed * 0.25f), 0f, 0.75f);
        GetComponent<AudioSource>().pitch = 0.5f + Mathf.Abs(currentSpeed * 0.075f);

        Vector2 v2 = ((transform.position + (Vector3)facingDirection) - transform.position).normalized;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle = 360 + angle;
        angle = 360 - angle;
        GetComponent<Animator>().SetFloat("Direction", angle);
    }
}
