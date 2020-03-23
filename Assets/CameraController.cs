using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    float desiredPerfectSize;

    public CameraZone currentZone;
    public float lerpFactor = 2.0f;


    void Awake() 
    {
        cam = GetComponent<Camera>();
    }

    public void SetZone(CameraZone zone) 
    {
        currentZone = zone;
    }

    void Update()
    {
        float pixelRatio = 1.0f;
        while(true) 
        {
            desiredPerfectSize = ((Screen.height)/(pixelRatio * 16.0f)) * 0.5f;

            // Once too small, scale it up
            if(desiredPerfectSize < currentZone.desiredHeight / 2f) 
            {
                desiredPerfectSize = ((Screen.height)/((pixelRatio - 1.0f) * 16.0f)) * 0.5f;
                break;
            }
            pixelRatio = pixelRatio + 1.0f;
        }
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredPerfectSize, Time.deltaTime * lerpFactor);
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(currentZone.position.x, currentZone.position.y, -10.0f), Time.deltaTime * lerpFactor);
    }
}
