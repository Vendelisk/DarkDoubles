using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpawn : MonoBehaviour
{
    public List<GameObject> spawnPoints;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child.gameObject);
        }

        if (PersistentValues.Instance.checkpoint1)
        {
            spawnPoints[0].SetActive(false);
            spawnPoints[1].SetActive(true);
        }
    }
}
