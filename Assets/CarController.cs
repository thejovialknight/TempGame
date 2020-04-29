using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float direction = 0f;

    FreeMovement movement;

    public float turnSpeed;

    void Awake()
    {
        movement = GetComponent<FreeMovement>();
    }

    void Start()
    {
        GetComponent<Animator>().SetFloat("Direction", direction);
    }

    void Update()
    {
        GetComponent<Animator>().SetFloat("Direction", direction);
        Vector2 facingDirection = new Vector2(Mathf.Cos(direction), Mathf.Sin(direction));
        Vector2 directionVector = facingDirection * Input.GetAxis("Vertical");
        movement.Move(directionVector);

        direction += Input.GetAxis("Horizontal") * turnSpeed * directionVector.magnitude * Time.deltaTime;

        Vector2 v2 = ((transform.position + (Vector3)facingDirection) - transform.position).normalized;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle = 360 + angle;
        angle = 360 - angle;
        GetComponent<Animator>().SetFloat("Direction", angle);
    }
}
