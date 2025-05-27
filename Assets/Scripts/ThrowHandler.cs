using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHandler : MonoBehaviour
{
    public PinManager pinManager; 
    private bool throwInProgress = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!throwInProgress && collision.relativeVelocity.magnitude > 1.5f)
        {
            throwInProgress = true;
            Debug.Log("Ball thrown!");
            Invoke(nameof(EvaluatePins), 3f);
        }
    }

    void EvaluatePins()
    {
        pinManager.EvaluatePins(); 
        gameObject.SetActive(false);
        throwInProgress = false;
    }
}