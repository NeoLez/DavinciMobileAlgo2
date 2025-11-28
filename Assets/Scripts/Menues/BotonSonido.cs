using UnityEngine;
using UnityEngine.UI;

public class BotonSonido : MonoBehaviour
{
    public AudioSource miAudioSource;
    public AudioClip sonidoClick;

    public void ReproducirSonido()
    {
        // PlayOneShot permite que el sonido se solape si clickeas muy rápido
        miAudioSource.PlayOneShot(sonidoClick);
    }
}