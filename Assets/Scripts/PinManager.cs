using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Presets;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public List<GameObject> pins;
    public GameObject bowlingBall;
    public Transform ballStartPoint;

    public void ResetPinsAndBall()
    {
        // Reset pins
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

        // Hide the ball
        if (bowlingBall != null)
        {
            bowlingBall.SetActive(false);
        }
    }

    public void StartEvaluationAfterThrow()
    {
        StartCoroutine(WaitForPinsToSettle());
    }

    private IEnumerator WaitForPinsToSettle()
    {
        yield return new WaitForSeconds(3f);

        int uprightPins = 0;
        foreach (var pin in pins)
        {
            float angle = Vector3.Angle(pin.transform.up, Vector3.up);
            if (angle < 5f)
            {
                uprightPins++;
            }
        }

        int fallenPins = pins.Count - uprightPins;
        Debug.Log($"EvaluatePins() → Fallen pins: {fallenPins}");

        FindObjectOfType<ScoreManager>().RegisterThrow(fallenPins);
    }
}
