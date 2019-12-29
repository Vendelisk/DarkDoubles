using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSwdDmg : MonoBehaviour
{
    private float hitCD = 1.5f;
    private float remCD = 0f;
    public AudioSource hitSound;

    private void Update()
    {
        remCD -= Time.deltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy") && remCD <= 0)
        {
            PlayerCombat.Instance.DealDamage(other.GetComponent<Enemy>());
            hitSound.Play();
            remCD = hitCD;
        }
    }
}
