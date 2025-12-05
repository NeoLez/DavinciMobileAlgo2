using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Root
{
    public class SoundFXManager : MonoBehaviour
    {

        public static SoundFXManager Instance;
        [SerializeField] private AudioSource soundFXobject;


        private void Awake()
        {
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(soundFXobject);

            if (Instance == null)
            {

                Instance = this;
            }
            
        }


       public void PlaySound(AudioClip audioclip, Transform spawnTransform , float volume)
        {

            
            AudioSource audioSource = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);

            // Forzar activación del objeto y sus padres
            Transform t = audioSource.transform;
            while (t != null)
            {
                t.gameObject.SetActive(true);
                t = t.parent;
            }
            audioSource.enabled = true;

            //assign the audioclip
            audioSource.clip = audioclip;

            //assign volume
            audioSource.volume = volume;

            // play the sound
            audioSource.Play();

            // get length of clip
            float clipLength = audioSource.clip.length;

            // destroy gameObject after its done playing
            Destroy(audioSource.gameObject, clipLength);

        }



    }
}
