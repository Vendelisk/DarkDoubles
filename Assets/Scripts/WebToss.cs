using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebToss : MonoBehaviour
{
    public GameObject web;
    private Animator anim;
    private Camera cam;

    private bool inProg;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main; // this is heavier than it pretends to be
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxis("Spell") > .2f) && !InGameMenu.Instance.paused && !inProg)
        {
            inProg = true;
            anim.SetBool("Web", true);
            transform.rotation = new Quaternion(transform.rotation.x, cam.transform.rotation.y, transform.rotation.z, cam.transform.rotation.w);
        }
    }

    private void CastStartEvent()
    {
        web.SetActive(true);
        anim.SetBool("Web", false);
    }

    private void CastEndEvent()
    {
        web.SetActive(false);
        StartCoroutine(ProgSwitch(.8f));
    }

    IEnumerator ProgSwitch(float wait)
    {
        yield return new WaitForSeconds(wait);
        inProg = false;
    }
}
