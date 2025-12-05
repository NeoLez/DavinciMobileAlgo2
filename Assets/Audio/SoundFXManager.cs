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

            // spawn in gameObject
            AudioSource audioSource = Instantiate(soundFXobject , spawnTransform.position, Quaternion.identity);  //get the AudioSource component

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
