using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class S_VolumeManager : MonoBehaviour//Gemaakt door Ruben, zodat je de volume kan aanpassen.
{
    public AudioMixer mainAudioMixer;

    public void ChangeAudioVolume(float audioVolume)
    {
        mainAudioMixer.SetFloat("masterVolume", audioVolume);
    }
}
