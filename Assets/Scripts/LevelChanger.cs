using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    private string nextLvl;
    private GameObject takeToNext;
    public Vector3 spawnLoc;
    private GameObject[] toRemove;
    //private AsyncOperation aScene;
    //private Scene currScene;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SceneManager.sceneLoaded += AfterLoad;
        //currScene = SceneManager.GetActiveScene();
    }

    private void AfterLoad(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= AfterLoad;
        if (scene.name != "Main Scene")
            PersistentValues.Instance.clearBlock = true;
        if (PersistentValues.Instance.clearBlock)
        {
            toRemove = GameObject.FindGameObjectsWithTag("RemoveOnLoad");
            foreach (GameObject obj in toRemove)
            {
                Destroy(obj);
            }
        }
    }

    public void FadeToLevel (string lvl, GameObject keeper)
    {
        nextLvl = lvl;
        takeToNext = keeper;
        anim.SetTrigger("FadeOut");
    }

    public void FadeOutEndEvent()
    {
        //print("playing");
        //StartCoroutine(ProgressiveLoadScene());
        SceneManager.LoadScene(nextLvl);
        if (PlayerCombat.Instance != null)
            PlayerCombat.Instance.gameObject.transform.position = spawnLoc;
        //SceneManager.MoveGameObjectToScene(takeToNext, SceneManager.GetSceneByName(nextLvl));
        //SceneManager.UnloadSceneAsync(currScene);
    }

    //IEnumerator ProgressiveLoadScene()
    //{
    //    aScene = SceneManager.LoadSceneAsync(nextLvl, LoadSceneMode.Additive);
    //    aScene.allowSceneActivation = false;

    //    // loading
    //    //aScene.completed += SceneLoaded;

    //    while (!aScene.isDone)
    //    {
    //        print("Loading progress: " + (aScene.progress * 100) + "%");
    //    }

    //    yield return null;

    //    //aScene.allowSceneActivation = true;
    //    //SceneManager.MoveGameObjectToScene(takeToNext, SceneManager.GetSceneByName(nextLvl));
    //    //SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextLvl));
    //}

    //private void SceneLoaded(AsyncOperation obj)
    //{
    //    // finished loading
    //    aScene.allowSceneActivation = true;
    //    SceneManager.MoveGameObjectToScene(takeToNext, SceneManager.GetSceneByName(nextLvl));
    //    SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextLvl));
    //}
}
