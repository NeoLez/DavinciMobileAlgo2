using System;
using Root.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Root
{
    public class BotonIniciarNivel : MonoBehaviour
    {
        [SerializeField] private string nombreEscenaNivel;
        [SerializeField] private int staminaCost;

        [Header("Popup Settings")]
        [SerializeField] private ConfirmationPopup confirmationPopup;
        [SerializeField] private string translationKey;
        [SerializeField] private bool requireConfirmation = true;

        private void Awake()
        {
            staminaCost = RemoteManager.GetInt("levelStaminaCost");
        }

        // Este es el metodo que ya tenes enganchado en el OnClick del Inspector
        public void ComenzarNivel()
        {
            if (requireConfirmation && confirmationPopup != null)
            {
                // Muestro el cartel primero
                confirmationPopup.ShowPopup(translationKey, RealComenzarNivel);
            }
            else
            {
                RealComenzarNivel();
            }
        }

        private void RealComenzarNivel()
        {
            // Aca mantengo tu logica original:
            // Recien cuando el jugador confirmo, intento cobrarle la estamina.
            // Si no le alcanza, hace return y no carga la escena.
            if (!Database.Database.Ins.staminaSystem.ConsumeStamina(staminaCost)) return;

            LoadingScreen.Instance.LoadScene(nombreEscenaNivel);
        }
    }
}