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

    private UnityAction actionToConfirm;
    private string currentTranslationKey;

    private void Start()
    {
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);

        if (Localization.Ins != null)
        {
            Localization.Ins.OnUpdate += UpdatePopupText;
        }
    }

    private void OnDestroy()
    {
        if (Localization.Ins != null)
        {
            Localization.Ins.OnUpdate -= UpdatePopupText;
        }
    }

    public void ShowPopup(string localizationKey, UnityAction action)
    {
        popupPanel.SetActive(true);
        currentTranslationKey = localizationKey;
        actionToConfirm = action;

        UpdatePopupText();
    }

    private void UpdatePopupText()
    {
        if (Localization.Ins != null && Localization.Ins.IsInitialized() && !string.IsNullOrEmpty(currentTranslationKey))
        {
            string translatedText = Localization.Ins.GetTranslate(currentTranslationKey);

            // Si la traduccion devuelve vacio, muestro la KEY cruda en pantalla.
            // Asi sabemos que el popup funciona y el problema es la planilla.
            if (string.IsNullOrEmpty(translatedText))
            {
                messageText.text = $"[{currentTranslationKey} - ERROR TRADUCCION]";
                Debug.LogWarning($"Gemini: El Localization no encontro texto para la key '{currentTranslationKey}'.");
            }
            else
            {
                messageText.text = translatedText;
            }
        }
        else if (!string.IsNullOrEmpty(currentTranslationKey))
        {
            // Si el Localization no arranco, muestro la key
            messageText.text = currentTranslationKey;
        }
    }

    private void OnYesClicked()
    {
        popupPanel.SetActive(false);
        actionToConfirm?.Invoke();
    }

    private void OnNoClicked()
    {
        popupPanel.SetActive(false);
        actionToConfirm = null;
    }
}