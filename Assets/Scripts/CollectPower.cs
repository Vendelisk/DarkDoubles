using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectPower : MonoBehaviour
{
    public TextMeshPro infoTxt;
    public string power;

    // Start is called before the first frame update
    void Start()
    {
        if (PersistentValues.Instance.firePower && power.Equals("Fire"))
            gameObject.SetActive(false);
        else if (PersistentValues.Instance.webPower && power.Equals("Web"))
            gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Collect"))
        {
            if (power.Equals("Fire"))
            {
                PersistentValues.Instance.firePower = true;
                if (other.GetComponent<FlameBreath>())
                    other.GetComponent<FlameBreath>().enabled = true;
                gameObject.SetActive(false);
            }
            else if (power.Equals("Web"))
            {
                PersistentValues.Instance.webPower = true;
                if (other.GetComponent<WebToss>())
                    other.GetComponent<WebToss>().enabled = true;
                gameObject.SetActive(false);
            }
            infoTxt.enabled = false;
        }

        else if (other.CompareTag("Player"))
        {
            infoTxt.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            infoTxt.enabled = false;
    }
}
