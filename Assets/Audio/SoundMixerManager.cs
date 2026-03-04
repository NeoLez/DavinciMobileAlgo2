using UnityEngine;
using UnityEngine.Audio;

namespace Root
{
    public class SoundMixerManager : MonoBehaviour
    {
        public static SoundMixerManager Instance;
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
        


