using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailSortManager : Minigame
{
    public float maxTimer = 30.0f;
    public float currentTimer;
    public int score = 0;
    public int targetIndex;
    public List<Sprite> slotSprites = new List<Sprite>();
    public AudioClip correctSound;
    public AudioClip wrongSound;

    public ConveyorManager conveyorManager;
    public SpriteRenderer conveyorSlot;
    public Text timerText;
    public Text scoreText;

    public override void StartGame()
    {
        base.StartGame();

        MessageEventManager.OnSelectEvent += OnSelect;

        currentTimer = maxTimer;
        GenerateSlot();
    }

    public override void UpdateGame()
    {
        base.UpdateGame();

        currentTimer -= Time.deltaTime;
        timerText.text = Mathf.Round(currentTimer).ToString();
        scoreText.text = score.ToString();

        if(currentTimer <= 0.0f)
        {
            EndGame();
        }
    }

    public override void EndGame()
    {
        MessageEventManager.OnSelectEvent -= OnSelect;

        Debug.Log("End game!");

        // implement at end of here otherwise will become inactive
        base.EndGame();
    }

    void GenerateSlot()
    {
        targetIndex = Random.Range(0, slotSprites.Count);
        conveyorSlot.sprite = slotSprites[targetIndex];
    }

    void OnSelect(string id, int selected)
    {
        if(id == "MAIL_SORT")
        {
            if(selected == targetIndex)
            {
                score++;
                GenerateSlot();
                AudioSource.PlayClipAtPoint(correctSound, Camera.main.transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(wrongSound, Camera.main.transform.position);
            }
        }
    }
}
