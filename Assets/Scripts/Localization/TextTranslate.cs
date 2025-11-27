using TMPro;
using UnityEngine;

namespace Root
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextTranslate : MonoBehaviour
    {
        [SerializeField] string _id;

        TMP_Text _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<TMP_Text>();

            Localization.Ins.OnUpdate += Refresh;

        }

        void Refresh()
        {
            var result = Localization.Ins.GetTranslate(_id);

            _textComponent.text = result;
        }
    }
}