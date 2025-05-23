using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Presets;
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
        int uprightPins = 0;

        foreach (var pin in pins)
        {
            float angle = Vector3.Angle(pin.transform.up, Vector3.up);
            if (angle < 5f) // Still standing
            {
                uprightPins++;
            }
        }

        int fallenPins = pins.Count - uprightPins;

        Debug.Log($"EvaluatePins() → Fallen pins: {fallenPins}");

        if (scoreManager != null)
        {
            scoreManager.RegisterThrow(fallenPins);
        }
        else
        {
            Debug.LogWarning("scoreManager is not assigned!");
        }
    }
}
