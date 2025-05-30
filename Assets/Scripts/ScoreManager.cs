using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreboardText;
    private int totalScore = 0;
    private int throwCount = 0;

    public void Start()
    {
        if(scoreboardText == null)
        {
            scoreboardText = GetComponentInChildren<TMP_Text>();
        }
        UpdateScoreboard();
    }
    public void RegisterThrow(int pinsKnocked)
    {
        throwCount++;
        totalScore += pinsKnocked;

        UpdateScoreboard();

        FindObjectOfType<PinManager>().ResetPinsAndBall();

        if (throwCount >= 2)
        {
            throwCount = 0;
            scoreboardText.text += "\nFrame Complete!";
        }
    }

    void UpdateScoreboard()
    {
        if (scoreboardText != null)
        {
            scoreboardText.text = $"Total Score: {totalScore}\nThrows in Frame: {throwCount}/2";
        }
    }
}
