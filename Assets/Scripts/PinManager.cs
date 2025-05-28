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
    private bool throwInProgress = false;

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

    public void StartEvaluationAfterThrow()
    {
        StartCoroutine(WaitForPinsToSettle());
    }

    private IEnumerator WaitForPinsToSettle()
    {
        yield return new WaitForSeconds(1.5f);

        float settleTime = 0f;
        const float settleThreshold = 0.1f; // how still pins must be
        const float waitTime = 2.0f; // total time to wait before checking again

        while (true)
        {
            bool allStill = true;

            foreach (var pin in pins)
            {
                if (pin == null) continue;

                Rigidbody rb = pin.GetComponent<Rigidbody>();
                if (rb == null || !rb.IsSleeping())
                {
                    allStill = false;
                    break;
                }
            }

            if (allStill)
            {
                break; // pins are settled
            }

            yield return new WaitForSeconds(waitTime);
            settleTime += waitTime;

            if (settleTime > 10f) // fallback
            {
                Debug.LogWarning("Pins didn’t settle in time. Scoring anyway.");
                break;
            }
        }

        EvaluatePins(); // Only evaluate after all pins are still
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
