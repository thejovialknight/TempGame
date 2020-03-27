using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    CameraController cameraController;

    public bool isActive;

    public CameraZone zone;

    void Update()
    {
        if (isActive)
        {
            UpdateGame();
        }
    }

    void OnEnable ()
    {
        StartGame();
        isActive = true;
    }

    public virtual void StartGame()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.StartZoneOverride(zone, true);

        MessageEventManager.RaiseOnPause();
    }

    public virtual void UpdateGame()
    {

    }

    public virtual void EndGame()
    {
        cameraController.EndZoneOverride(true);

        MessageEventManager.RaiseOnResume();

        gameObject.SetActive(false);
    }
}
