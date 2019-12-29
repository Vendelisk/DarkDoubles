using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantSpider : Enemy
{
    public GiantSpider() : base(150f, 5f, 0f, 5f, 8f) { }

    private AudioSource[] feet;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        feet = GetComponentsInChildren<AudioSource>();
    }

    public override void Die()
    {
        anim.Play("Death");
        GetComponent<Patroller>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        base.Die();

        StartCoroutine(ReverseKinematic(2f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            DealDamage();
    }

    private void SpiderStepSound()
    {
        foreach (AudioSource foot in feet)
        {
            foot.Play();
        }
    }

    IEnumerator ReverseKinematic(float wait)
    {
        yield return new WaitForSeconds(wait);
        GetComponent<Rigidbody>().isKinematic = !GetComponent<Rigidbody>().isKinematic;
    }
}
