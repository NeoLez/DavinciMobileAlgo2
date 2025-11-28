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
            audioMixer.SetFloat("Global Volumen", level);
        }


        public void SetMusicVolume(float level)
        {
            audioMixer.SetFloat("Music Volumen", level);
        }


        public void SetSFXsVolume(float level)
        {
            audioMixer.SetFloat("Sound FXs Volumen", level);
        }   


    }
}
        


