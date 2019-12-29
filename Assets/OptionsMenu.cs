using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenTog;
    public Dropdown qualityDD;
    public Dropdown resDD;
    public Slider volSlider;
    public AudioMixer am;

    private Resolution[] res;
    private string currBtn;

    private void Start()
    {
        res = Screen.resolutions;
        resDD.ClearOptions();

        int curRes = 0;
        List<string> options = new List<string>();
        foreach(Resolution resolution in res)
        {
            string opt = resolution.width + " x " + resolution.height;
            options.Add(opt);

            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
                curRes = options.Count - 1;
        }

        resDD.AddOptions(options);
        resDD.value = curRes;
        resDD.RefreshShownValue();


        volSlider.onValueChanged.AddListener(SetVolume);
        qualityDD.onValueChanged.AddListener(SetQuality);
        fullscreenTog.onValueChanged.AddListener(SetFullscreen);
        resDD.onValueChanged.AddListener(SetResolution);
    }

    // SEE SetButton BELOW
    //private void Update()
    //{
    //    if (Input.anyKeyDown)
    //    {
    //        if (Input.GetKeyDown("joystick 1 button 0")) // a
    //            currBtn = "joystick 1 button 0";
    //        else if (Input.GetKeyDown("joystick 1 button 1")) // b
    //            currBtn = "joystick 1 button 1";
    //        else if (Input.GetKeyDown("joystick 1 button 2")) // x
    //            currBtn = "joystick 1 button 2";
    //        else if (Input.GetKeyDown("joystick 1 button 3")) // y
    //            currBtn = "joystick 1 button 3";
    //        else if (Input.GetKeyDown("joystick 1 button 4")) // lb
    //            currBtn = "joystick 1 button 4";
    //        else if (Input.GetKeyDown("joystick 1 button 5")) // rb
    //            currBtn = "joystick 1 button 5";
    //        else if (Input.GetKeyDown("joystick 1 button 6")) // select
    //            currBtn = "joystick 1 button 6";
    //        else if (Input.GetKeyDown("joystick 1 button 7")) // start
    //            currBtn = "joystick 1 button 7";
    //        else if (Input.GetKeyDown("joystick 1 button 8")) // ls
    //            currBtn = "joystick 1 button 8";
    //        else if (Input.GetKeyDown("joystick 1 button 9")) // rs
    //            currBtn = "joystick 1 button 9";
    //        else
    //            currBtn = Input.inputString;
    //        print(currBtn);
    //    }
    //}

    public void SetVolume(float vol)
    {
        am.SetFloat("Volume", vol);
    }

    public void SetQuality(int pickIdx)
    {
        QualitySettings.SetQualityLevel(pickIdx);
    }

    public void SetFullscreen(bool fs)
    {
        Screen.fullScreen = fs;
    }

    public void SetResolution(int resIdx)
    {
        Resolution desRes = res[resIdx];
        Screen.SetResolution(desRes.width, desRes.height, Screen.fullScreen);
    }

    //public void SetButton()
    //{
    //    // Apparently this isn't even possible soooooo..... fuck me, right?
    //    // Not creating a whole custom input manager this late into dev... I'M NOT EVEN BEING PAID FOR THIS D:
    //}
}
