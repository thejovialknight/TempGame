using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    CameraController cameraController;

    public string id;
    public enum GameState { Intro, InGame, Outro };
    public GameState state;
    public int score = 0;
    public int rating = 0;
    public int bestScore = 0;
    public int bestRating = 0;
    
    public CameraZone zone;
    public GameObject introScreen;
    public Text bestScoreText;
    public Text bestRatingText;
    public GameObject outroScreen;
    public Text outroScoreText;
    public Text outroRatingText;
    public GameObject gameUI;
    public Text scoreText;

    void Start()
    {
        GameManager.manager.RegisterMinigame(this);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (state == GameState.Intro)
        {
            UpdateIntro();
        }
        else if (state == GameState.InGame)
        {
            UpdateGame();
        }
        else if (state == GameState.Outro)
        {
            UpdateOutro();
        }
    }

    void OnEnable ()
    {
        MessageEventManager.OnReceiveMessageEvent += OnReceiveMessage;
    }

    void OnDisable()
    {
        MessageEventManager.OnReceiveMessageEvent -= OnReceiveMessage;
    }

    void OnReceiveMessage(string message)
    {
        if(message == id)
        {
            ShowIntro();
        }
    }

    public virtual void ShowIntro() {
        state = GameState.Intro;

        MessageEventManager.RaiseOnPause();

        bestScoreText.text = bestScore.ToString();
        bestRatingText.text = bestRating.ToString();

        outroScreen.SetActive(false);
        gameUI.SetActive(false);
        introScreen.SetActive(true);
    }
    
    public virtual void StartGame()
    {
        state = GameState.InGame;

        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.StartZoneOverride(zone, true);

        score = 0;
        rating = 0;

        gameUI.SetActive(true);
    }

    public virtual void ShowOutro() 
    {
        state = GameState.Outro;

        SetRating();

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

        MessageEventManager.RaiseOnResume();

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
