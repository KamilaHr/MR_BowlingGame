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
            pinManager.StartEvaluationAfterThrow();
            StartCoroutine(DisableBallAfter(4f));
        }
    }

    private IEnumerator DisableBallAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
        throwInProgress = false;
    }
}