using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene : MonoBehaviour
{
    private PlayableDirector cutscene;

    // Start is called before the first frame update
    void Start()
    {
        cutscene = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            cutscene.time = cutscene.duration - .25f;
        }
    }
}
