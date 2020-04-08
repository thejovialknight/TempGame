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
                MessageEventManager.RaiseOnPause(true, true);
                pauseObject.SetActive(true);
            }
            else if(!isPaused) {
                MessageEventManager.RaiseOnResume();
                pauseObject.SetActive(false);
            }
        }
    }
}
