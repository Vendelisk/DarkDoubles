using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //next scene
        SceneManager.LoadScene(1); // Main Level
    }
    
    public void QuitGame()
    {
        Application.Quit(); // Only works in build
        //UnityEditor.EditorApplication.isPlaying = false; // Only works in editor
    }
}
