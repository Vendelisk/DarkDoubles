using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public float maxHealth = 100;
    public float currHealth = 100;
    public float koan = 1;
    public float speed = 1;
    public float recovery = 1;
    public float power = 25;

    private Animator anim;
    public bool dead;

    public Image bloodPattern;
    public Image redFill;
    public GameObject deathMenu;

    #region Singleton

    private static PlayerCombat instance;
    public static PlayerCombat Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    private void Start()
    {
        deathMenu = InGameMenu.Instance.deathMenuUI;
        anim = GetComponent<Animator>();
        InvokeRepeating("Heal", 1f, 2f);

        if (PersistentValues.Instance != null)
            PersistentValues.Instance.SpawnCache();
    }

    public void TakeDamage(float dmg)
    {
        if (currHealth - dmg > 0)
        {
            currHealth -= dmg;
            bloodPattern.color = new Color(bloodPattern.color.r, bloodPattern.color.g, bloodPattern.color.b, (100 - currHealth) / 200f);
            redFill.color = new Color(redFill.color.r, redFill.color.g, redFill.color.b, (100 - currHealth) / 200f);
        }
        else if (!dead)
        {
            dead = true;
            Die();
        }
    }

    public void DealDamage(Enemy enemy)
    {
        enemy.TakeDamage(power * koan);
    }

    public void Die()
    {
        anim.Play("Die");
        StartCoroutine(DelayMenu(2f));
        dead = true;
        PersistentValues.Instance.DeathCache(maxHealth, koan, speed, power, gameObject.GetComponent<FlameBreath>().enabled, GetComponent<WebToss>().enabled, gameObject.GetComponent<WindSword>().enabled);
        GetComponent<FlameBreath>().enabled = false;

        //SceneManager.LoadScene("Main Scene"); // just load main scene
    }

    private IEnumerator DelayMenu(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        deathMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (transform.position.y < -50f) // insurance
            TakeDamage(5000f);
        if(deathMenu.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Heal()
    {
        if (currHealth < maxHealth)
        {
            currHealth += recovery;
            bloodPattern.color = new Color(bloodPattern.color.r, bloodPattern.color.g, bloodPattern.color.b, (100 - currHealth) / 200f);
            redFill.color = new Color(redFill.color.r, redFill.color.g, redFill.color.b, (100 - currHealth) / 200f);
        }

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
            bloodPattern.color = new Color(bloodPattern.color.r, bloodPattern.color.g, bloodPattern.color.b, 0);
            redFill.color = new Color(redFill.color.r, redFill.color.g, redFill.color.b, 0);
        }
    }
}
