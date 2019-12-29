using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float maxHealth;
    protected float health;
    protected float power;
    [Range(0,1)]
    protected float dropRate;
    [Range(0,1)]
    protected float turnSpeed = .2f;
    protected float attackRange;
    public float attackCD;
    public float remCD = 0f;
    public bool isDead = false;
    public GameObject itemParticles;
    protected Item dropped = null;
    private bool lootInProgress = false;
    protected Animator anim;
    protected Rigidbody rb;

    protected virtual void Update()
    {
        remCD -= Time.deltaTime;
        if (remCD <= 0)
            anim.SetBool("Attack", false);
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        itemParticles.SetActive(false);
    }

    public Enemy(float hp, float pwr, float drpRt)
    {
        health = hp;
        power = pwr;
        dropRate = drpRt;
        maxHealth = health;
    }

    public Enemy(float hp, float pwr, float drpRt, float atkRng, float atkCD)
    {
        health = hp;
        power = pwr;
        dropRate = drpRt;
        maxHealth = health;
        attackCD = atkCD;
        attackRange = atkRng;
    }

    public virtual void Attack(Vector3 direction)
    {
        if (remCD <= 0  && !isDead)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed);
            transform.LookAt(direction);
            //if(!isTurning)
            //    StartCoroutine(SmoothLook(1f, direction));
            anim.SetBool("Attack", true);
            remCD = attackCD;
        }
    }

    public virtual void Die()
    {
        anim.SetBool("Dead", true);
        isDead = true;
        GetComponent<Rigidbody>().isKinematic = true;
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
            col.enabled = false;
        //colliders = GetComponentsInChildren<Collider>();
        //foreach (Collider col in colliders)
        //    col.enabled = false;

        dropped = RandomItemDrop.Instance.DropRandomItem();
        //dropped = RandomItemDrop.Instance.DropItem("HealthPot");
        if (dropped != null)
            itemParticles.SetActive(true);
    }

    public void Loot()
    {
        if (!lootInProgress && dropped != null)
        {
            lootInProgress = true;
            Inventory.Instance.Add(dropped);
            itemParticles.SetActive(false);
            dropped = null;
            lootInProgress = false;
        }
    }

    public virtual void TakeDamage(float amount)
    {
        //anim.SetTrigger("Hit");
        health -= amount;
        if (health <= 0)
            Die();
    }

    public void DealDamage()
    {
        PlayerCombat.Instance.TakeDamage(power);
    }

    public float GetAtkRange()
    {
        return this.attackRange;
    }

    public void Move(float speed, Vector3 direction)
    {
        if (!isDead)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime);
            transform.LookAt(direction);
            //if(!isTurning)
            //    StartCoroutine(SmoothLook(1f, direction));
            anim.SetFloat("Forward", speed * .2f);
        }
    }
    public void Move(float speed)
    {
        if (!isDead)
        {
            anim.SetFloat("Forward", speed * .2f);
        }
    }

    protected IEnumerator SmoothLook(float duration, Vector3 direction)
    {
        float startTime = Time.time;
        while(Time.time < startTime + duration)
        {
            if (!isDead)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed);
            yield return null;
        }
        if (!isDead)
            transform.LookAt(direction);
    }
}
