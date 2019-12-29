using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IlluminationCascade : MonoBehaviour
{
    public GameObject mainLightTrigger;
    private int sequenceNum;

    // Start is called before the first frame update
    void Awake()
    {
        sequenceNum = int.Parse(Regex.Match(gameObject.name, @"\d+").Value);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneReady;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneReady;
    }

    private void SceneReady(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(LightOn(sequenceNum * .4f));
    }

    private IEnumerator LightOn(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
        if (mainLightTrigger != null)
            mainLightTrigger.SetActive(true);
    }
}
