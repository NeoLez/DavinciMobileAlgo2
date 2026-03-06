using TMPro;
using UnityEngine;

namespace Root
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextTranslate : MonoBehaviour
    {
        [SerializeField] string _id;

        TMP_Text _textComponent;

        private void Start()
        {
            _textComponent = GetComponent<TMP_Text>();

            // Verificamos si la instancia existe antes de usarla
            if (Localization.Ins != null)
            {
                if (Localization.Ins.IsInitialized())
                {
                    Refresh();
                }
                Localization.Ins.OnUpdate += Refresh;
            }
            else
            {
                Debug.LogWarning($"Localization.Ins no encontrado para el objeto {gameObject.name}. Asegurate de que el Localization Manager esté en la escena.");
            }
        }

        private void OnDestroy()
        {
            // SIEMPRE desuscribite de los eventos para evitar errores de memoria o nulos al cambiar de escena
            if (Localization.Ins != null)
            {
                Localization.Ins.OnUpdate -= Refresh;
            }
        }

        public void Refresh()
        {
            if (_textComponent == null) _textComponent = GetComponent<TMP_Text>();

            var result = Localization.Ins.GetTranslate(_id);
            if (!string.IsNullOrEmpty(result))
            {
                _textComponent.text = result;
            }
        }

        public void SetId(string id)
        {
            _id = id;
            if (_textComponent is null) _textComponent = GetComponent<TMP_Text>();
            if (Localization.Ins != null && Localization.Ins.IsInitialized()) Refresh();
        }
    }
}