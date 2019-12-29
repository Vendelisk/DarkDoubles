using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource slideSound;
    public AudioSource rollSound;
    public AudioSource[] footsteps;
    private AudioSource camAudio;
    public AudioClip[] bkgMusic; // 0 is start menu, 1 is lvl 1, etc etc

    private void Start()
    {
        camAudio = Camera.main.GetComponent<AudioSource>();
        SceneManager.sceneLoaded += SceneHasLoaded;
    }

    private void SceneHasLoaded(Scene scene, LoadSceneMode mode)
    {
        camAudio = Camera.main.GetComponent<AudioSource>();
        camAudio.clip = bkgMusic[SceneManager.GetActiveScene().buildIndex];
        camAudio.Play();
    }

    private void SlideSound()
    {
        slideSound.Play();
    }

    private void FootstepSound()
    {
        footsteps[Random.Range(0, footsteps.Length)].Play();
    }

    private void RollSound()
    {
        rollSound.Play();
    }
}
