using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSnapToGrid : MonoBehaviour
{
    public bool isSnapped = true;
    public bool toggleButton;

    void OnValidate()
    {
        if (isSnapped)
        {
            float xPos = Mathf.Round(transform.position.x * 2f) / 2f;
            float yPos = Mathf.Round(transform.position.y * 2f) / 2f;

            transform.position = new Vector3(xPos, yPos, transform.position.z);
        }
    }
}
