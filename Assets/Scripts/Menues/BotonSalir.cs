using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    [SerializeField] private ConfirmationPopup confirmationPopup;

    // Creo esta variable para poder escribir la ID de traduccion desde Unity
    // por ejemplo: "ask_quit_game"
    [SerializeField] private string translationKey;

    public void OnClickQuit()
    {
        // Le mando la clave de traduccion y la accion real a ejecutar
        confirmationPopup.ShowPopup(translationKey, RealQuit);
    }

    private void RealQuit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}