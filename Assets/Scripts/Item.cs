using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = null;
    public string variety = null;
    public Sprite icon = null;
    public int level = 1;
    public string type = null; // health, koan, speed
    public string description = null; // tooltip info
    //[SerializeField] private float frequency = 1f; // how often (sec) effect is triggered
    [SerializeField] private float duration = 10f; // how long (sec) effect lasts
    [SerializeField] private float mult = 1f;

    public void Effect()
    {
        float change;
        switch (type)
        {
            case "health":
                switch (variety)
                {
                    case "pot":
                        // delegates and lambdas and headaches - oh my!
                        change = (level * mult * 2f);
                        Inventory.Instance.StartCoroutine(Revert((diff) => PlayerCombat.Instance.recovery += diff, change));
                        break;
                    case "buff":
                        PlayerCombat.Instance.maxHealth += (level * mult * 15f);
                        PlayerCombat.Instance.currHealth += (level * mult * 15f);
                        break;
                }
                break;
            case "koan":
                switch (variety)
                {
                    case "pot":
                        change = (level * mult / 10f);
                        Inventory.Instance.StartCoroutine(Revert((diff) => PlayerCombat.Instance.koan += diff, change));
                        break;
                    case "buff":
                        PlayerCombat.Instance.koan += (level * mult / 10f);
                        break;
                }
                break;
            //case "speed":
            //    switch (variety)
            //    {
            //        case "pot":
            //            break;
            //        case "buff":
            //            break;
            //    }
            //    break;
            default:
                return;
        }

        Inventory.Instance.Remove(this);
    }

    public IEnumerator Revert(System.Action<float> initVar, float change)
    {
        initVar(change);
        yield return new WaitForSeconds(duration);
        initVar(change * -1f);
    }
}
