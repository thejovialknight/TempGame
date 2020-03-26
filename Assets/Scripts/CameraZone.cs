using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public Vector2 position;
    [Range(2f, 50f)]public float desiredHeight;
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) 
        {
            Camera.main.GetComponent<CameraController>().SetZone(this);
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(position, new Vector3(desiredHeight * 1.85f, desiredHeight, 1.0f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(position, new Vector3(desiredHeight * 1.33f, desiredHeight, 1.0f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(position, new Vector3(desiredHeight, desiredHeight, 1.0f));
    }
}
