using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCambiarEscena : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private ConfirmationPopup confirmationPopup;
    [SerializeField] private string translationKey;

    // Aca me creo la variable para el tilde. 
    // La pongo en true por defecto para que de base me pida confirmacion.
    [SerializeField] private bool requireConfirmation = true;

    public void ChangeSceneOnClick()
    {
        // Me fijo si el tilde esta activado en el Inspector
        if (requireConfirmation)
        {
            // Si tiene el tilde, llamo al popup como veniamos haciendo
            if (confirmationPopup != null)
            {
                confirmationPopup.ShowPopup(translationKey, RealChangeScene);
            }
        }
        else
        {
            // Si le saque el tilde, cambio de escena al instante sin preguntar nada
            RealChangeScene();
        }
    }

    private void RealChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}