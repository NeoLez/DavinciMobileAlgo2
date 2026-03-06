using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ConfirmationPopup : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    // Aca guardo la funcion que quiero ejecutar y la key por si cambia el idioma
    private UnityAction actionToConfirm;
    private string currentTranslationKey;

    private void Start()
    {
      
        // Asigno mis funciones a los botones por codigo
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);

        // Me suscribo al evento de actualizacion de idioma de mi Localization
        if (Localization.Ins != null)
        {
            Localization.Ins.OnUpdate += UpdatePopupText;
        }

        // Obligo al popup a traducirse apenas se activa por primera vez
        UpdatePopupText();
    }

    private void OnDestroy()
    {
        // Me desuscribo cuando el objeto se destruye para evitar fugas de memoria
        if (Localization.Ins != null)
        {
            Localization.Ins.OnUpdate -= UpdatePopupText;
        }
    }

    public void ShowPopup(string localizationKey, UnityAction action)
    {
        // 1. PRIMERO prendo el panel para que TextMeshPro despierte
        popupPanel.SetActive(true);

        // 2. DESPUES le paso la key y actualizo el texto
        currentTranslationKey = localizationKey;
        UpdatePopupText();

        // 3. Guardo la accion
        actionToConfirm = action;
    }

    private void UpdatePopupText()
    {
        // Reviso que mi Localization este listo antes de pedirle el texto
        if (Localization.Ins != null && Localization.Ins.IsInitialized() && !string.IsNullOrEmpty(currentTranslationKey))
        {
            // Uso el metodo GetTranslate de mi script
            messageText.text = Localization.Ins.GetTranslate(currentTranslationKey);
        }
    }

    private void OnYesClicked()
    {
        popupPanel.SetActive(false);
        // Ejecuto la accion guardada si no esta vacia
        actionToConfirm?.Invoke();
    }

    private void OnNoClicked()
    {
        // Cierro el panel y limpio la accion
        popupPanel.SetActive(false);
        actionToConfirm = null;
    }
}