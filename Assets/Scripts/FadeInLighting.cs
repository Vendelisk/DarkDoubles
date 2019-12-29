using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInLighting : MonoBehaviour
{
    private Light mainLight;

    // Start is called before the first frame update
    void Start()
    {
        mainLight = GetComponent<Light>();
    }

    private void FixedUpdate()
    {
        if (mainLight.intensity < 1f)
            mainLight.intensity += .01f;
    }
}
