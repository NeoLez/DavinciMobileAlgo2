using UnityEngine;
using UnityEngine.SceneManagement;

// Script para cambiar de escena con un botón
public class BotonCambiarEscena : MonoBehaviour
{
    [SerializeField] private string nombreEscena; // Nombre de la escena a cargar

 
    public void CambiarEscenaOnClick() // Método para cambiar de escena al hacer clic en el botón
    {
        SceneManager.LoadScene(nombreEscena);
    }
}