using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinTracker : MonoBehaviour
{
    private GameObject[] pins;
    private int knockedDownCount;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        pins = GameObject.FindGameObjectsWithTag("BowlingPin");
    }

    void Update()
    {
        knockedDownCount = 0;

        foreach(GameObject pin in pins)
        {
            if (pin == null) continue;

            float tilt = Vector3.Angle(Vector3.up, pin.transform.up);
            if (tilt > 20f)
                knockedDownCount++;
        }
        scoreText.text = "Pins knocked down: " + knockedDownCount;
        Debug.Log("Pins knocked down: " + knockedDownCount);
    }
}
