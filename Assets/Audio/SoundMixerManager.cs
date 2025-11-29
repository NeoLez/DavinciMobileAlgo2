using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Root
{
    public class SoundMixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;

        public void SetGlobalVolume(float level)
        {
            audioMixer.SetFloat("Global Volumen", Mathf.Log10(level) * 20f);
        }


        public void SetMusicVolume(float level)
        {
            audioMixer.SetFloat("Music Volumen", Mathf.Log10(level) * 20f);
        }


        public void SetSFXsVolume(float level)
        {
            audioMixer.SetFloat("Sound FXs Volumen", Mathf.Log10(level) * 20f);
        }   


    }
}
        


