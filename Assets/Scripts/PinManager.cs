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

    public GameObject bowlingBall;
    public Transform ballStartPoint;

    public void ResetPins()
    {
        foreach (var pin in pins)
        {
            pin.SetActive(false);
            pin.transform.position = pin.GetComponent<PinReset>().initialPosition; 
            pin.transform.rotation = pin.GetComponent<PinReset>().initialRotation;

            Rigidbody rb = pin.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();

            pin.SetActive(true);
        }

        // Reset the ball
        if (bowlingBall != null && ballStartPoint != null)
        {
            bowlingBall.transform.position = ballStartPoint.position;
            bowlingBall.transform.rotation = ballStartPoint.rotation;

            var rb = bowlingBall.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            bowlingBall.SetActive(true);
        
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
