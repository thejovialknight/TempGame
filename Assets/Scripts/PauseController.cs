using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    bool isPaused = false;

    public GameObject pauseObject;

    void Update() {
        if(Input.GetButtonDown("Pause")) {
            isPaused = !isPaused;
            if(isPaused) {
                GameManager.Pause(true, true);
                pauseObject.SetActive(true);
            }
            else if(!isPaused) {
                GameManager.Resume();
                pauseObject.SetActive(false);
            }
        }
    }
}
