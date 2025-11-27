using TMPro;
using UnityEngine;

namespace Root
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextTranslate : MonoBehaviour
    {
        [SerializeField] string _id;

        [SerializeField] Localization _localization;

        TMP_Text _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<TMP_Text>();

            _localization.OnUpdate += Refresh;

        }

        void Refresh()
        {
            var result = _localization.GetTranslate(_id);

            _textComponent.text = result;
        }
    }
}