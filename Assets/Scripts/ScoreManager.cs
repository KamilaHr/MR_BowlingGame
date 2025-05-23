using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public int[] scores = new int[10]; 
    public int currentFrame = 0; 
    public int throwInFrame = 0; 
    public TMP_Text scoreboardText;

    private int[] frameThrows = new int[21]; 
    private int throwIndex = 0;

    void Start()
    {
        if (scoreboardText != null)
        {
            scoreboardText.text = "Testing";
        
            Debug.Log("ScoreboardText updated in Start()");
        }
        else
        {
            Debug.LogWarning("ScoreboardText is not assigned.");
        }
    }

    public void RegisterThrow(int fallenPins)
    {
        frameThrows[throwIndex++] = fallenPins;

        if (throwInFrame == 0)
        {
            if (fallenPins == 10) // Strike
            {
                currentFrame++;
                throwInFrame = 0;
            }
            else
            {
                throwInFrame = 1;
            }
        }
        else
        {
            currentFrame++;
            throwInFrame = 0;
        }

        UpdateScoreboard();
    }

    void UpdateScoreboard()
    {
        if (scoreboardText == null)
        {
            Debug.LogWarning("ScoreboardText is not assigned.");
            return;
        }
        string display = "";
        int score = 0;
        int throwPos = 0;

        for (int frame = 0; frame < currentFrame && frame < 10; frame++)
        {
            if (throwPos >= throwIndex)
            {
                display += $"Frame {frame + 1}: Pending...\n";
                break;
            }

            int first = frameThrows[throwPos];
            int second = (throwPos + 1 < throwIndex) ? frameThrows[throwPos + 1] : 0;
            int third = (throwPos + 2 < throwIndex) ? frameThrows[throwPos + 2] : 0;

            if (first == 10)
            {
                score += 10 + second + third;
                display += $"Frame {frame + 1}: Strike ({score})\n";
                throwPos += 1;
            }
            else if (first + second == 10)
            {
                score += 10 + third;
                display += $"Frame {frame + 1}: Spare ({score})\n";
                throwPos += 2;
            }
            else
            {
                score += first + second;
                display += $"Frame {frame + 1}: {first} + {second} = {score}\n";
                throwPos += 2;
            }
        }

        if (string.IsNullOrWhiteSpace(display))
        {
            display = "Waiting for throws...";
        }

        scoreboardText.text = display;
        Debug.Log("Updated scoreboard with:\n" + display);
    }
}
