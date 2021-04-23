using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixerMaster;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private string volumeIdentifierMaster;
    
    [Space]
    [SerializeField] private AudioMixer mixerMusic;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private string volumeIdentifierMusic;
    
    [Space] 
    [SerializeField] private AudioMixer mixerSFX;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private string volumeIdentifierSFX;
    

    void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat(volumeIdentifierMaster, 0.75f);
        sliderMusic.value = PlayerPrefs.GetFloat(volumeIdentifierMusic, 0.75f);
        sliderSFX.value = PlayerPrefs.GetFloat(volumeIdentifierSFX, 0.75f);
    }
    public void SetLevelMaster (float sliderValue)
    {
        mixerMaster.SetFloat(volumeIdentifierMaster, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(volumeIdentifierMaster, sliderValue);
    }
    
    public void SetLevelMusic (float sliderValue)
    {
        mixerMaster.SetFloat(volumeIdentifierMusic, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(volumeIdentifierMusic, sliderValue);
    }
    
    public void SetLevelSFX (float sliderValue)
    {
        mixerMaster.SetFloat(volumeIdentifierSFX, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(volumeIdentifierSFX, sliderValue);
    }
}
