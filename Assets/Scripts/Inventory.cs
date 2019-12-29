using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int maxItems = 10;

    public delegate void inventoryChanged();
    public inventoryChanged updateInventory;

    #region Singleton

    private static Inventory instance;
    public static Inventory Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    #endregion

    public void Add (Item item)
    {
        if (items.Count < maxItems)
        {
            if (updateInventory != null)
            {
                items.Add(item);
                updateInventory.Invoke();
            }
        }
        else
            return; // TODO: add message to screen or audio clip play
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if (updateInventory != null)
        {
            updateInventory.Invoke();
        }
    }

    public void UseItem (Item item)
    {
        item.Effect();
        items.Remove(item);

        if (updateInventory != null)
            updateInventory.Invoke();
    }
}
