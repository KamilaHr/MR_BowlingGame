using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public Frame currentFrame;
    public TMP_Text scoreboardText;

    private List<Frame> frames = new List<Frame>();
    private int frameIndex = 0;

    void Start()
    {
        frames.Clear();
        for(int i = 0; i < 10; i++)
        {
            var f = new Frame();
            f.isTenthFrame = (i == 9);
            frames.Add(f);
        }
        currentFrame = frames[0];
    }

    void ResetPins()
    {
        FindObjectOfType<PinManager>().ResetPins();
    }

    public void RegisterThrow(int pinsKnocked)
    {
        if(frameIndex >= frames.Count) { scoreboardText.text = "Game Over!"; return; }

        if (currentFrame.firstThrow == -1)
        {
            currentFrame.firstThrow = pinsKnocked;
        }
        else if (currentFrame.secondThrow == -1)
        {
            currentFrame.secondThrow = pinsKnocked;
        }
        else if (currentFrame.isTenthFrame && currentFrame.thirdThrow == -1)
        {
            currentFrame.thirdThrow = pinsKnocked;
        }

        if (currentFrame.isComplete)
        {
            frameIndex++;
            if (frameIndex < frames.Count)
                currentFrame = frames[frameIndex];
            Invoke(nameof(ResetPins), 2f);
        }

        UpdateScoreboard();
    }

    void UpdateScoreboard()
    {
        string display = ""; 
        int totalScore = 0;

        for (int i = 0; i < frames.Count; i++)
        {
            var f = frames[i];
            Frame next1 = (i + 1 < frames.Count) ? frames[i + 1] : null;
            Frame next2 = (i + 2 < frames.Count) ? frames[i + 2] : null;

            int frameScore = f.GetScore(next1, next2);
            totalScore += frameScore;

            display += $"Frame {i + 1}: ";

            if (f.firstThrow == -1)
            {
                display += "--";
            }
            else if (f.isStrike)
            {
                display += "X";
            }
            else
            {
                display += f.firstThrow.ToString();
            }

            if (f.secondThrow == -1)
            {
                display += " - ";
            }
            else if (f.isSpare)
            {
                display += " / ";
            }
            else
            {
                display += " " + f.secondThrow;
            }

            if (f.isTenthFrame && f.thirdThrow != -1)
            {
                display += " + " + f.thirdThrow;
            }

            display += $" = {totalScore}\n";
        }

        if (scoreboardText != null)
            scoreboardText.text = display;
    }
}
