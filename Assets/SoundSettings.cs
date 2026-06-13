using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void UpdateMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", value);
    }

    public void UpdateSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", value);
    }
}
