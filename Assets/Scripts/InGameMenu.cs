using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject deathMenuUI;
    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;
    private CamLogic camSpin;
    private InventorySlot[] slots;
    public Transform slotsParent;

    public GameObject playerPrefab;
    public Transform playerSpawn;

    public bool paused = false;

    // also instantiates player at spawn point
    #region Singleton

    private static InGameMenu instance;
    public static InGameMenu Instance { get { return instance; } }

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("Spawn").transform;
        Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        camSpin = Camera.main.GetComponent<CamLogic>();

        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    private void Start()
    {
        Inventory.Instance.updateInventory += UpdateInventoryUI;
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        camSpin = Camera.main.GetComponent<CamLogic>();
    }

    // called before start, after awake
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("EndGameScene"))
            return;
        PlayableDirector[] timelines = GameObject.FindObjectsOfType<PlayableDirector>();
        if (timelines != null)
        {
            foreach (PlayableDirector timeline in timelines)
            {
                if (timeline.name.Equals("Sword-Cutscene") && !PersistentValues.Instance.windPower)
                    continue;
                else if (timeline.name.Equals("Boss-Cutscene") && !PersistentValues.Instance.bossScene)
                {
                    PersistentValues.Instance.bossScene = true;
                    continue;
                }
                timeline.enabled = false;
            }
        }
        if (playerSpawn == null)
            playerSpawn = GameObject.FindGameObjectWithTag("Spawn").transform;
        if (PlayerCombat.Instance == null)
            Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        if (camSpin == null)
            camSpin = Camera.main.GetComponent<CamLogic>();
        playerSpawn = GameObject.FindGameObjectWithTag("Spawn").transform;
        if (Inventory.Instance.updateInventory == null)
            Inventory.Instance.updateInventory += UpdateInventoryUI;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!controlsMenuUI.activeInHierarchy && (Input.GetButtonDown("Inventory") || (paused && Input.GetButtonDown("Menu"))))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                inventoryUI.SetActive(true);
                Pause();
            }
        }
        else if (Input.GetButtonDown("Menu"))
        {
            pauseMenuUI.SetActive(true);
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        inventoryUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camSpin.enabled = true;
    }

    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        camSpin.enabled = false;
        Time.timeScale = 0f;
        paused = true;
    }

    public void QuitGame()
    {
        Application.Quit(); // Only works in build
        //UnityEditor.EditorApplication.isPlaying = false; // Only works in editor
    }

    public void PlayAgain()
    {
        Destroy(GameObject.FindWithTag("Player"));
        Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        deathMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (i < Inventory.Instance.items.Count)
                slots[i].UpdateSlot(Inventory.Instance.items[i]);
            else
                slots[i].ClearSlot();
        }
    }
}
