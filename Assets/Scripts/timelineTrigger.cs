using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class timelineTrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject origSpawn;
    public GameObject newSpawn;
    private static GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PersistentValues.Instance.checkpoint1)
        {
            timeline.Play();
            PersistentValues.Instance.checkpoint1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    public void SwordTimelineStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        //AnimationPlayableBinding.Create("test", GameObject.FindGameObjectWithTag("Player").transform.root.gameObject);
    }

    public void SwordTimelineEnd()
    {
        player.GetComponent<WindSword>().enabled = true;
        player.SetActive(true);
    }
}
