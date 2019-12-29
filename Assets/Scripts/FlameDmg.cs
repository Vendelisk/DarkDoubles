using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDmg : MonoBehaviour
{
    public GameObject mutantParent;
    private Mutant mutant;
    private bool enemyParent;
    private float cd = .5f;
    private float remCD = 0f;

    private void Start()
    {
        if (mutantParent)
            mutant = mutantParent.GetComponent<Mutant>();
        enemyParent = transform.root.gameObject.CompareTag("Enemy");
    }

    private void Update()
    {
        remCD -= Time.deltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player") && enemyParent) // you are enemy, other is player
            mutant.DealDamage();
        else if (other.CompareTag("Enemy") && !enemyParent && remCD <= 0)
        { // you are player, other is enemy
            PlayerCombat.Instance.DealDamage(other.GetComponent<Enemy>());
            remCD = cd;
        }
        else if (other.CompareTag("Flamable") && !enemyParent) // you are player, other should burn
            other.GetComponent<Dissolve>().dissolve = true;
    }
}
