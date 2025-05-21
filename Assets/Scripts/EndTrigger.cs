using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public PinManager pinManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BowlingBall"))
        {
            Debug.Log("Ball entred the end zone");
            pinManager.EvaluatePins();
        }
    }
}
