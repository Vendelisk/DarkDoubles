using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebShot : MonoBehaviour
{
    public GameObject webPlatform;
    public ParticleSystem flyingWeb;
    List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        flyingWeb = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnEnable()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(flyingWeb, other, collisionEvents); // populates list of events

        if (other.CompareTag("Lava"))
        {
            for (int i = 0; i < collisionEvents.Count; i++)
            {
                SpawnAt(collisionEvents[i]);
            }
        }
    }

    private void SpawnAt(ParticleCollisionEvent pce)
    {
        Instantiate(webPlatform, pce.intersection, Quaternion.LookRotation(pce.normal));
    }
}
