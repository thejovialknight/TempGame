using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        int keyPressed = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            keyPressed = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            keyPressed = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            keyPressed = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            keyPressed = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            keyPressed = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            keyPressed = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            keyPressed = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            keyPressed = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            keyPressed = 9;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            keyPressed = 10;
        }

        if(keyPressed != -1) {
            if(GameManager.Inventory.Count > keyPressed - 1) {
                GameManager.SetActiveItem(GameManager.Inventory[keyPressed - 1]);
            }
            else {
                GameManager.ClearActiveItem();
            }
        }
    }
}
