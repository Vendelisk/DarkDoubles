using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    private float damageCD = .5f;
    private float remCD = 0f;

    // Update is called once per frame
    void Update()
    {
        remCD -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && remCD <= 0f)
        {
            PlayerCombat.Instance.TakeDamage(55);
            remCD = damageCD;
        }

        else if (other.tag == "Enemy" && !other.isTrigger)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(40);
        }
    }
}
