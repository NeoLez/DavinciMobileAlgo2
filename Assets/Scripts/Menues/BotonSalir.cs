using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    [SerializeField] private ConfirmationPopup confirmationPopup;

    // Aca pongo el ID exacto que tengo en mi CSV (ej: "popup_quit_game")
    [SerializeField] private string translationKey;

    public void OnClickQuit()
    {
        // Llamo al popup pasandole mi ID de texto y mi funcion de salir
        confirmationPopup.ShowPopup(translationKey, RealQuit);
    }

    private void RealQuit()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}