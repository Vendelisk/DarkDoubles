using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentValues : MonoBehaviour
{
    public float maxHealth = 100;
    public float currHealth = 100;
    public float koan = 1;
    public float speed = 1;
    public float recovery = 1;
    public float power = 25;

    public bool windPower;
    public bool firePower;
    public bool webPower;

    public bool bossScene;
    public bool checkpoint1;

    public List<Item> items;

    public bool clearBlock = false;

    public Color[] fogColor; // just used for minimap

    #region Singleton

    private static PersistentValues instance;
    public static PersistentValues Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    public void DeathCache(float hpMax, float k, float s, float pwr, bool fire, bool web, bool wind)
    {
        maxHealth = hpMax;
        koan = k;
        speed = s;
        power = pwr;
        firePower = fire;
        webPower = web;
        windPower = wind;
        this.items = Inventory.Instance.items;
    }

    public void SpawnCache()
    {
        PlayerCombat.Instance.maxHealth = this.maxHealth;
        PlayerCombat.Instance.currHealth = this.currHealth;
        PlayerCombat.Instance.koan = this.koan;
        PlayerCombat.Instance.speed = this.speed;
        PlayerCombat.Instance.recovery = this.recovery;
        PlayerCombat.Instance.power = this.power;
        PlayerCombat.Instance.gameObject.GetComponent<WindSword>().enabled = windPower;
        PlayerCombat.Instance.gameObject.GetComponent<FlameBreath>().enabled = firePower;
        PlayerCombat.Instance.gameObject.GetComponent<WebToss>().enabled = webPower;
        Inventory.Instance.items = this.items;
    }
}
