using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManager : Minigame
{
    public float maxTimer = 30.0f;
    public float currentTimer;

    public TextMeshProUGUI timerText;
    public Transform truckTransform;

    public override void StartGame()
    {
        base.StartGame();

        cameraController.SetFollowing(truckTransform);
        currentTimer = maxTimer;
    }

    public override void UpdateGame()
    {
        base.UpdateGame();

        currentTimer -= Time.deltaTime;
        timerText.text = Mathf.Round(currentTimer).ToString();
        scoreText.text = score.ToString();

        if (currentTimer <= 0.0f)
        {
            ShowOutro();
        }
    }

    public override void EndGame()
    {
        cameraController.ClearFollowing();

        // implement at end of here otherwise will become inactive
        base.EndGame();
    }

    public override void SetRating()
    {
        rating = Mathf.Clamp(score / 5, 1, 3);
    }
}
