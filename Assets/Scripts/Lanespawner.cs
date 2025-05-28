using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanespawner : MonoBehaviour
{
    public GameObject lanePrefab;
    public Transform spawnAnchor;

    private GameObject spawnedLane;
    void Start()
    {
        if (lanePrefab != null && spawnAnchor != null)
        {
            spawnedLane = Instantiate(lanePrefab, spawnAnchor.position, spawnAnchor.rotation);
        }
        else
        {
            Debug.LogWarning("LanePrefab or SpawnAnchor not assigned.");
        }
    }

   
}
