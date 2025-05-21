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
            pinManager.EvaluatePins();
        }
    }
}
