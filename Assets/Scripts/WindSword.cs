using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSword : MonoBehaviour
{
    public GameObject sword;
    public GameObject swordHeavy;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Mod") && Input.GetButtonDown("Attack") || Input.GetButtonDown("AttackHeavy"))
            anim.SetBool("AttackHeavy", true);
        else if (Input.GetButtonDown("Attack"))
            anim.SetBool("Attack", true);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            sword.SetActive(true);
            anim.SetBool("Attack", false);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackHeavy"))
        {
            swordHeavy.SetActive(true);
            anim.SetBool("AttackHeavy", false);
        }
        else
        {
            swordHeavy.SetActive(false);
            sword.SetActive(false);
        }
    }
}
