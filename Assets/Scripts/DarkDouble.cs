using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarkDouble : Enemy
{
    public DarkDouble() : base(500f, 20f, 0f, 2f, 3f) { }

    public float attack2CD;
    public GameObject meteorPrefab;
    public GameObject flameWallPrefab;
    public GameObject levelSwitch;
    public ParticleSystem footFire;
    public ParticleSystem footFireEmbers;
    public AudioSource[] footsteps;
    private bool attacking;

    private void Awake()
    {
        if (PersistentValues.Instance.bossScene)
            BattleTimelineEnd();
    }

    protected override void Start()
    {
        anim = GetComponent<Animator>();
        base.Start();
        attack2CD = 5f;
    }

    public override void Die()
    {
        anim.Play("Die");
        base.Die();
        ParticleSystem.MainModule ff = footFire.main;
        ParticleSystem.MainModule ffe = footFireEmbers.main;
        ff.loop = false;
        ffe.loop = false;
        StartCoroutine(EndGame(5f));
    }

    public override void Attack(Vector3 direction)
    {
        if (!attacking)
        {
            attacking = true;
            transform.LookAt(direction);
            if(Random.Range(0, 2) == 0)
            {
                anim.SetBool("Attack", true);
                remCD = attackCD;
            }
            else
            {
                anim.SetBool("Attack2", true);
                remCD = attack2CD;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (remCD <= 0)
        {
            anim.SetBool("Attack2", false);
            attacking = false;
        }
    }

    //public override void TakeDamage(float amount)
    //{
    //    base.TakeDamage(amount);
    //    anim.Play("Recoil");
    //}

    private void MeteorEvent()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Instantiate(meteorPrefab, playerPos, Quaternion.identity);
    }

    private void FlameWallEvent()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        GameObject fireWall = Instantiate(flameWallPrefab, playerPos, Quaternion.identity);
        StartCoroutine(DestroyLater(15f, fireWall));
    }

    private IEnumerator DestroyLater(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }

    private IEnumerator EndGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        levelSwitch.SetActive(true);
    }

    public void BattleTimelineStart()
    {
        GetComponent<Patroller>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }

    public void BattleTimelineEnd()
    {
        GetComponent<Patroller>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
    }

    private void FootstepSound()
    {
        footsteps[Random.Range(0, footsteps.Length)].Play();
    }
}
