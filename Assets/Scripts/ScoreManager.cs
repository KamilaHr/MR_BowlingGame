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
        /*string display = "";
        int score = 0;
        int i = 0;

        for (int frame = 0; frame < currentFrame && frame < 10; frame++)
        {
            if (frameThrows[i] == 10)
            {
                score += 10 + frameThrows[i + 1] + frameThrows[i + 2];
                display += $"Frame {frame + 1}: Strike ({score})\n";
                i += 1;
            }
            else if (frameThrows[i] + frameThrows[i + 1] == 10)
            {
                score += 10 + frameThrows[i + 2];
                display += $"Frame {frame + 1}: Spare ({score})\n";
                i += 2;
            }
            else
            {
                score += frameThrows[i] + frameThrows[i + 1];
                display += $"Frame {frame + 1}: {frameThrows[i]} + {frameThrows[i + 1]} = {score}\n";
                i += 2;
            }
        }

        if (scoreboardText != null)
            scoreboardText.text = display;*/
        if (scoreboardText != null)
        {
            scoreboardText.text = "Frame updated!";
        }
        else
        { Debug.LogWarning("ScoreboardText is not assigned"); }
    }
}
