using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBreath : MonoBehaviour
{
    public GameObject fire;
    private Animator anim;
    private bool inProg;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Attack2") || (Input.GetAxis("Attack2") > .2f)) && !InGameMenu.Instance.paused && !inProg)
        {
            inProg = true;
            anim.SetBool("Fire", true);
        }
    }

    private void AttackStartEvent()
    {
        fire.SetActive(true);
        anim.SetBool("Fire", false);
    }

    private void AttackEndEvent()
    {
        fire.SetActive(false);
        inProg = false;
    }
}
