using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private LevelChanger lc;
    private string levelToLoad;

    private void Start()
    {
        lc = GetComponentInChildren<LevelChanger>();
        levelToLoad = gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            lc.FadeToLevel(levelToLoad, other.gameObject);
    }
}
