using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamewallDmg : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        print("firewall collision");
        if (other.CompareTag("Player"))
        {
            PlayerCombat.Instance.TakeDamage(1f);
            print("hitting player");
        }
    }
}
