using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    SpriteRenderer sr;
    public bool dissolve;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(dissolve)
        {
            if (sr.color.a > 0)
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - .05f);
            else
                Destroy(gameObject);
        }
    }
}
