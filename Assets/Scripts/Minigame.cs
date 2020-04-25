using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Minigame : MonoBehaviour
{
    CameraController cameraController;

    public string id;
    public enum MinigameState { Intro, InGame, Outro };
    public MinigameState state;
    public int score = 0;
    public int rating = 0;
    public int bestScore = 0;
    public int bestRating = 0;
    public string oneStarMessage = "Bad";
    public string twoStarMessage = "Good";
    public string threeStarMessage = "Amazing";
    
    public CameraZone zone;
    public GameObject introScreen;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestRatingText;
    public GameObject outroScreen;
    public TextMeshProUGUI outroMessage;
    public TextMeshProUGUI outroScoreText;
    public TextMeshProUGUI outroRatingText;
    public GameObject gameUI;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (state == MinigameState.Intro)
        {
            UpdateIntro();
        }
        else if (state == MinigameState.InGame)
        {
            UpdateGame();
        }
        else if (state == MinigameState.Outro)
        {
            UpdateOutro();
        }
    }

    void OnEnable ()
    {
        MessageEventManager.OnMinigameStart += OnMinigameStart;
        MessageEventManager.OnJobRegister += OnJobRegister;
    }

    void OnDisable()
    {
        MessageEventManager.OnMinigameStart -= OnMinigameStart;
        MessageEventManager.OnJobRegister -= OnJobRegister;
    }

    void OnMinigameStart(string id)
    {
        if(id == this.id)
        {
            ShowIntro();
        }
    }

    public void OnJobRegister() {
        GameManager.RegisterMinigame(this);
        gameObject.SetActive(false);
    }

    public void LoadData(MinigameData dataToLoad) {
        bestScore = dataToLoad.score;
        bestRating = dataToLoad.rating;
    }

    public MinigameData SaveData() {
        MinigameData dataToSave = new MinigameData();
        dataToSave.id = id;
        dataToSave.score = bestScore;
        dataToSave.rating = bestRating;
        return dataToSave;
    }

    public virtual void ShowIntro() {
        state = MinigameState.Intro;

        GameManager.State = GameState.Minigame;
        GameManager.Pause(true, true);

        bestScoreText.text = bestScore.ToString();
        bestRatingText.text = bestRating.ToString();

        outroScreen.SetActive(false);
        gameUI.SetActive(false);
        introScreen.SetActive(true);
    }
    
    public virtual void StartGame()
    {
        state = MinigameState.InGame;

        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.StartZoneOverride(zone, true);

        score = 0;
        rating = 0;

        gameUI.SetActive(true);
    }

    void SetRatingMessage() {
        switch(rating) {
            case 1:
                outroMessage.text = oneStarMessage;
                break;
            case 2:
                outroMessage.text = oneStarMessage;
                break;
            case 3:
                outroMessage.text = threeStarMessage;
                break;
            default:
                break;
        }
    }

    public virtual void ShowOutro() 
    {
        state = MinigameState.Outro;

        SetRating();
        SetRatingMessage();

        if(score > bestScore) {
            bestScore = score;
        }

        if(rating > bestRating) {
            bestRating = rating;
        }

        outroScoreText.text = score.ToString();
        outroRatingText.text = rating.ToString();

        gameUI.SetActive(false);
        outroScreen.SetActive(true);
    }

    public virtual void EndGame()
    {
        cameraController.EndZoneOverride(true);
        GameManager.Resume();
        GameManager.ProgressTime();

        outroScreen.SetActive(false);
        introScreen.SetActive(false);
        gameObject.SetActive(false);
    }

    public virtual void UpdateIntro() {
        if(Input.GetButtonDown("Interact")) {
            introScreen.SetActive(false);
            StartGame();
        }
    }

    public virtual void UpdateGame()
    {

    }

    public virtual void UpdateOutro() 
    {
        if(Input.GetButtonDown("Interact")) {
            outroScreen.SetActive(false);
            EndGame();
        }
    }

    public virtual void SetRating() {
        
    }
}

[System.Serializable]
public class MinigameData
{
    public string id;
    public int score;
    public int rating;
}