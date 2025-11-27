using UnityEngine;

// Script para cerrar el juego con un botón
public class BotonSalir : MonoBehaviour
{
    // Método que se llama al hacer click en el botón
    public void OnClickQuit()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Cierra el juego
    }
}