using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public List<GameObject> pins; 
    private int lastFrameScore = 0; 
    public int currentFrame = 1; 
    public int throwCount = 0;

    public ScoreManager scoreManager;

    public void ResetPins()
    {
        foreach (var pin in pins)
        {
            pin.SetActive(true);
            pin.transform.rotation = Quaternion.identity;
            pin.transform.position = pin.GetComponent<PinReset>().initialPosition;
        }
    }

    public void EvaluatePins()
    {
        int fallenPins = 0;
        foreach (var pin in pins)
        {
            if (Vector3.Dot(pin.transform.up, Vector3.up) < 0.5f)
            {
                fallenPins++;
            }
        }

        scoreManager.RegisterThrow(fallenPins);
    }
}
