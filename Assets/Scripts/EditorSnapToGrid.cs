using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSnapToGrid : MonoBehaviour
{
    public bool isSnapped = true;
    public bool toggleButton;
    public float multiplier = 2f;

    void OnValidate()
    {
        if (isSnapped)
        {
            float xPos = Mathf.Round(transform.position.x * multiplier) / multiplier;
            float yPos = Mathf.Round(transform.position.y * multiplier) / multiplier;

            transform.position = new Vector3(xPos, yPos, transform.position.z);
        }
    }
}
