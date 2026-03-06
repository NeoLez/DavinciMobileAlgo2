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

        // YO: Agrego una key especifica para cuando falta energia
        [SerializeField] private string noStaminaTranslationKey = "ID_NO_STAMINA";
        [SerializeField] private bool requireConfirmation = true;

        private void Awake()
        {
            staminaCost = RemoteManager.GetInt("levelStaminaCost");
        }

        public void ComenzarNivel()
        {
            if (requireConfirmation && confirmationPopup != null)
            {
                confirmationPopup.ShowPopup(translationKey, RealComenzarNivel);
            }
            else
            {
                RealComenzarNivel();
            }
        }

        private void RealComenzarNivel()
        {
            // YO: Intento consumir la estamina. Si da false, no alcanza.
            if (!Database.Database.Ins.staminaSystem.ConsumeStamina(staminaCost))
            {
                // YO: Muestro el popup reciclando el mismo panel. Le paso una accion vacia () => {}
                if (confirmationPopup != null)
                {
                    confirmationPopup.ShowPopup(noStaminaTranslationKey, () => {
                        Debug.Log("Yo: Se cerro el aviso de falta de estamina.");
                    });
                }

                // Corto la ejecucion para que no cargue la escena
                return;
            }

            // Si paso el if anterior, es que habia estamina y ya se desconto. Cargo el nivel.
            LoadingScreen.Instance.LoadScene(nombreEscenaNivel);
        }
    }
}