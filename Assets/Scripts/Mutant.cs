using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;

public class Mutant : Enemy
{
    public GameObject fireBreath;
    public AudioSource[] footsteps;

    public Mutant() : base(75f, 10f, .2f, 5f, 3f) { }

    protected override void Start()
    {
        anim = GetComponent<Animator>();
        base.Start();
    }

    public override void Die()
    {
        anim.Play("Death");
        fireBreath.SetActive(false);
        base.Die();
        fireBreath.SetActive(false); // insurance
    }

    // NOTE! damage is dealt through the particle system
    private void AttackStartEvent()
    {
        fireBreath.SetActive(true);
    }

    private void AttackEndEvent()
    {
        fireBreath.SetActive(false);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (!isDead)
            anim.Play("Recoil");
        AttackEndEvent();
    }

    private void FootstepSound()
    {
        footsteps[Random.Range(0, footsteps.Length)].Play();
    }
}
