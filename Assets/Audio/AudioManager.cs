using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Root
{
    // Repositorio de AudioClips
    public class AudioClipRepository
    {
        private readonly Dictionary<string, AudioClip> _clips = new();
        public AudioClipRepository(IEnumerable<AudioManager.AudioClipEntry> entries)
        {
            foreach (var entry in entries)
            {
                if (!string.IsNullOrEmpty(entry.clipName) && entry.clip != null)
                    _clips[entry.clipName] = entry.clip;
            }
        }
        public AudioClip Get(string name) => _clips.TryGetValue(name, out var clip) ? clip : null;
    }

    // Repositorio de AudioSources
    public class AudioSourceRepository
    {
        private readonly Dictionary<string, AudioSource> _sources = new();
        public AudioSourceRepository(IEnumerable<AudioManager.AudioSourceEntry> entries)
        {
            foreach (var entry in entries)
            {
                if (!string.IsNullOrEmpty(entry.sourceName) && entry.source != null)
                    _sources[entry.sourceName] = entry.source;
            }
        }
        public AudioSource Get(string name) => _sources.TryGetValue(name, out var src) ? src : null;
    }

    // Repositorio de AudioMixers
    public class AudioMixerRepository
    {
        private readonly Dictionary<string, AudioMixer> _mixers = new();
        public AudioMixerRepository(IEnumerable<AudioManager.MixerEntry> entries)
        {
            foreach (var entry in entries)
            {
                if (!string.IsNullOrEmpty(entry.mixerName) && entry.mixer != null)
                    _mixers[entry.mixerName] = entry.mixer;
            }
        }
        public AudioMixer Get(string name) => _mixers.TryGetValue(name, out var mixer) ? mixer : null;
    }

    // Cambiado el nombre para evitar conflicto con UnityEngine.Audio.AudioMixer
    public class AudioManager : MonoBehaviour
    {
        [System.Serializable]
        public class MixerEntry
        {
            public string mixerName;
            public AudioMixer mixer;
        }

        [System.Serializable]
        public class AudioSourceEntry
        {
            public string sourceName;
            public AudioSource source;
        }

        [System.Serializable]
        public class AudioClipEntry
        {
            public string clipName;
            public AudioClip clip;
        }

        [Header("Mixers")]
        [SerializeField]
        private List<MixerEntry> mixers = new();
        [Header("Audio Sources")]
        [SerializeField]
        private List<AudioSourceEntry> audioSources = new();
        [Header("Audio Clips")]
        [SerializeField]
        private List<AudioClipEntry> audioClips = new();

        [SerializeField]
        private AudioSource sfxSource; // Fuente para efectos de sonido
        [SerializeField]
        private AudioSource musicSource; // Fuente para música

        private AudioClipRepository _clipRepo;
        private AudioSourceRepository _sourceRepo;
        private AudioMixerRepository _mixerRepo;

        private void Awake()
        {
            _clipRepo = new AudioClipRepository(audioClips);
            _sourceRepo = new AudioSourceRepository(audioSources);
            _mixerRepo = new AudioMixerRepository(mixers);
        }

        // Volumen para cualquier mixer y grupo
        public void SetVolume(string mixerName, string groupParameter, float volume)
        {
            var mixer = _mixerRepo.Get(mixerName);
            if (mixer != null)
                mixer.SetFloat(groupParameter, Mathf.Log10(Mathf.Clamp01(volume)) * 20);
            else
                Debug.LogWarning($"Mixer '{mixerName}' not found.");
        }

        // Silenciar cualquier mixer (MasterVolume por defecto)
        public void MuteMixer(string mixerName, bool mute, string groupParameter = "MasterVolume")
        {
            var mixer = _mixerRepo.Get(mixerName);
            if (mixer != null)
                mixer.SetFloat(groupParameter, mute ? -80f : 0f);
            else
                Debug.LogWarning($"Mixer '{mixerName}' not found.");
        }

        // Métodos rápidos para el primer mixer (retrocompatibilidad)
        public void SetMasterVolume(float volume)
        {
            if (mixers.Count > 0 && mixers[0].mixer != null)
                mixers[0].mixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp01(volume)) * 20);
        }
        public void SetMusicVolume(float volume)
        {
            if (mixers.Count > 0 && mixers[0].mixer != null)
                mixers[0].mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp01(volume)) * 20);
        }
        public void SetSFXVolume(float volume)
        {
            if (mixers.Count > 0 && mixers[0].mixer != null)
                mixers[0].mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp01(volume)) * 20);
        }
        public void MuteAll(bool mute)
        {
            if (mixers.Count > 0 && mixers[0].mixer != null)
                mixers[0].mixer.SetFloat("MasterVolume", mute ? -80f : 0f);
        }

        // Reproducir un efecto de sonido en cualquier AudioSource
        public void PlaySFX(AudioClip clip, string sourceName = null)
        {
            AudioSource targetSource = sfxSource;
            if (!string.IsNullOrEmpty(sourceName))
            {
                var src = _sourceRepo.Get(sourceName);
                if (src != null) targetSource = src;
            }
            if (targetSource != null && clip != null)
                targetSource.PlayOneShot(clip);
        }

        // Reproducir música en cualquier AudioSource
        public void PlayMusic(AudioClip clip, bool loop = true, string sourceName = null)
        {
            AudioSource targetSource = musicSource;
            if (!string.IsNullOrEmpty(sourceName))
            {
                var src = _sourceRepo.Get(sourceName);
                if (src != null) targetSource = src;
            }
            if (targetSource != null && clip != null)
            {
                targetSource.clip = clip;
                targetSource.loop = loop;
                targetSource.Play();
            }
        }

        // Reproducir un clip por nombre (SFX o música)
        public void PlayClipByName(string clipName, string sourceName = null, bool loop = false)
        {
            var clip = _clipRepo.Get(clipName);
            if (clip == null)
            {
                Debug.LogWarning($"AudioClip '{clipName}' not found.");
                return;
            }
            AudioSource targetSource = null;
            if (!string.IsNullOrEmpty(sourceName))
            {
                targetSource = _sourceRepo.Get(sourceName);
            }
            if (targetSource == null)
                targetSource = loop ? musicSource : sfxSource;
            if (targetSource != null)
            {
                if (loop)
                {
                    targetSource.clip = clip;
                    targetSource.loop = true;
                    targetSource.Play();
                }
                else
                {
                    targetSource.PlayOneShot(clip);
                }
            }
        }
    }
}
