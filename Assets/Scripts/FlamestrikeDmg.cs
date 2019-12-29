using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamestrikeDmg : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            PlayerCombat.Instance.TakeDamage(20f);
    }
}
