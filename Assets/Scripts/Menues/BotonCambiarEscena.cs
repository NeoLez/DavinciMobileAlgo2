using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCambiarEscena : MonoBehaviour
{
    [SerializeField] private string sceneName; // El nombre de mi escena
    [SerializeField] private ConfirmationPopup confirmationPopup; // Referencia a mi popup
    [SerializeField] private string translationKey; // Ej: "ask_return_menu"

    // Este es el metodo que llamo desde el evento OnClick() del boton en el Inspector
    public void ChangeSceneOnClick()
    {
        // En vez de cargar la escena de una, le paso el texto y mi funcion real
        confirmationPopup.ShowPopup(translationKey, RealChangeScene);
    }

    // Aca guardo la logica real, esta funcion solo se ejecuta si le doy a Yes en el popup
    private void RealChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}