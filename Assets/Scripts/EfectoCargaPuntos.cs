using UnityEngine;
using TMPro;
using System.Collections;

public class EfectoCargaPuntos : MonoBehaviour
{
    [Header("Configuración")]
    public float tiempoEntrePuntos = 0.5f;
    public int maximoPuntos = 3;

    private TMP_Text _textoUI;

    // Cambiamos el Start normal por un IEnumerator para poder pausarlo
    private IEnumerator Start()
    {
        
      
        yield return new WaitForSeconds(1f);

        //buscamos el texto y arrancamos la animación
        _textoUI = GetComponent<TMP_Text>();

        if (_textoUI != null)
        {
            StartCoroutine(AnimarPuntos());
        }
    }

    private IEnumerator AnimarPuntos()
    {
        int cantidadPuntos = 0;

        while (true)
        {
            // Leemos lo que haya escrito tu TextTranslate y le sacamos los puntos
            string palabraBase = _textoUI.text.TrimEnd('.');

            // Le sumamos los puntos de la animación
            _textoUI.text = palabraBase + new string('.', cantidadPuntos);

            cantidadPuntos++;
            if (cantidadPuntos > maximoPuntos)
            {
                cantidadPuntos = 0;
            }

            // Esperamos antes del próximo ciclo
            yield return new WaitForSeconds(tiempoEntrePuntos);
        }
    }
}