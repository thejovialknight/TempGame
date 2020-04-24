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
            if(!isPaused && GameManager.instance.gameState == GameState.Default) {
                GameManager.instance.gameState = GameState.PauseMenu;
                GameManager.Pause(true, true);
                pauseObject.SetActive(true);
            }
            else if(isPaused && GameManager.instance.gameState == GameState.PauseMenu) {
                GameManager.Resume();
                pauseObject.SetActive(false);
            }
        }
    }
}
