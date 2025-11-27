using UnityEngine;

// Script para cerrar el juego con un botón
public class BotonSalir : MonoBehaviour
{
    // Método que se llama al hacer click en el botón
    public void OnClickQuit()
    {
        Application.Quit(); // Cierra el juego
    }
}