using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoot : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Collect"))
        {
            enemy.Loot();
        }
    }
}
