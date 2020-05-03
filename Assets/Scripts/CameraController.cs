using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    float desiredPerfectSize;
    public bool immediateTrigger = true;

    public CameraZone currentZone;
    public CameraZone overrideZone;
    public bool isOverride = false;
    public float lerpFactor = 2.0f;
    public bool isFollowing;
    public Transform followTarget;


    void Awake() 
    {
        cam = GetComponent<Camera>();
    }

    public void SetZone(CameraZone zone) 
    {
        currentZone = zone;
    }

    public void StartZoneOverride(CameraZone zone, bool isImmediate) {
        isOverride = true;
        overrideZone = zone;

        if(isImmediate) {
            immediateTrigger = true;
        }
    }

    public void EndZoneOverride(bool isImmediate) {
        isOverride = false;

        if(isImmediate) {
            immediateTrigger = true;
        }
    }

    public void SetFollowing(Transform target)
    {
        followTarget = target;
        isFollowing = true;
    }

    public void ClearFollowing()
    {
        isFollowing = false;
    }

    void Update()
    {
        CameraZone zoneToUse;
        if(isOverride) {
            zoneToUse = overrideZone;
        }
        else {
            zoneToUse = currentZone;
        }

        float pixelRatio = 1.0f;
        while(true) 
        {
            desiredPerfectSize = ((Screen.height)/(pixelRatio * 16.0f)) * 0.5f;

            // Once too small, scale it up
            if(desiredPerfectSize < zoneToUse.desiredHeight / 2f) 
            {
                desiredPerfectSize = ((Screen.height)/((pixelRatio - 1.0f) * 16.0f)) * 0.5f;
                break;
            }
            pixelRatio = pixelRatio + 1.0f;
        }
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredPerfectSize, Time.deltaTime * lerpFactor);
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(zoneToUse.position.x, zoneToUse.position.y, -10.0f), Time.deltaTime * lerpFactor);

        if(immediateTrigger) {
            immediateTrigger = false;
            cam.orthographicSize = desiredPerfectSize;
            cam.transform.position = new Vector3(zoneToUse.position.x, zoneToUse.position.y, -10.0f);
        }

        if(isFollowing)
        {
            transform.position = new Vector3(followTarget.position.x, followTarget.position.y, -10.0f);
        }
    }
}
