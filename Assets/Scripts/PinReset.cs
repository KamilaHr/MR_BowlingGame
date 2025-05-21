using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinReset : MonoBehaviour
{
    [HideInInspector] public Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }
}
