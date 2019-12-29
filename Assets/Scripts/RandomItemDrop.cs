using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemDrop : MonoBehaviour
{
    public List<Item> items;
    public float[] weights = { 50f, 20f, 20f, 5f, 5f };
    private float total = 0;

    #region Singleton

    private static RandomItemDrop instance;
    public static RandomItemDrop Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    #endregion

    private void Start()
    {
        //items.Add(new Item(null, 0, null)); // first weight (most likely)
        //items.Add(new Item("pot", SceneManager.GetActiveScene().buildIndex, "health")); // 2nd
        //items.Add(new Item("pot", SceneManager.GetActiveScene().buildIndex, "koan")); // 3rd ...
        ////items.Add(new Item("pot", SceneManager.GetActiveScene().buildIndex, "speed"));
        //items.Add(new Item("buff", SceneManager.GetActiveScene().buildIndex, "health"));
        //items.Add(new Item("buff", SceneManager.GetActiveScene().buildIndex, "koan"));
        ////items.Add(new Item("buff", SceneManager.GetActiveScene().buildIndex, "speed"));

        foreach (float chance in weights)
            total += chance;
    }

    public Item DropRandomItem()
    {
        float rand = Random.Range(0, total);

        for (int i = 0; i < weights.Length; ++i)
        {
            if (rand <= weights[i])
                return items[i];
            else
                rand -= weights[i];
        }
        return null; // this wont happen
    }

    public Item DropItem(string name)
    {
        int index = items.FindIndex(1, a => a.name.Contains(name));
        return items[index];
    }
}
