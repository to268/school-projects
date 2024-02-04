using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class MenuSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    private Resolution[] resolutions;
    
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    
    public int ResolutionIndex
    {
        get { return ResolutionIndex; }
        set
        {
            ResolutionIndex = value;
            SetResolution(value);
        }
    }
    
    private List<string> resolutionsOptions = new List<string>();
    private int refreshRate;

    void Start()
    {
        resolutions = Screen.resolutions;
        refreshRate = resolutions[0].refreshRate;
        
        resolutionDropdown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + $" ({resolutions[i].refreshRate} Hz)";
            resolutionsOptions.Add(option);
        }

        resolutionsOptions.Reverse();
        resolutionDropdown.AddOptions(resolutionsOptions);
        resolutionDropdown.value = GameData.GetResolution();

        float volume = GameData.GetVolume();
        
        audioMixer.SetFloat("volume", volume);
        volumeSlider.value = volume;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        GameData.SetVolume(volume);
    }

    public void SetResolution(int index)
    {
        String res = resolutionsOptions[index];
        int width = Int32.Parse(res.Split('x')[0]);
        int height = Int32.Parse(res.Split('x')[1].Split('(')[0]);
        
        Screen.SetResolution(width, height, FullScreenMode.MaximizedWindow, refreshRate);
        GameData.SetResolution(index);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}