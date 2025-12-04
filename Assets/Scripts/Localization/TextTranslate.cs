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

            if (Localization.Ins.IsInitialized()) Refresh();
            Localization.Ins.OnUpdate += Refresh;
        }

        public void Refresh()
        {
            //if (string.IsNullOrEmpty(_id)) _id = _textComponent.text;
            var result = Localization.Ins.GetTranslate(_id);

            _textComponent.text = result;
        }

        public void SetId(string id)
        {
            _id = id;
            if (_textComponent is null) _textComponent = GetComponent<TMP_Text>();
            if(Localization.Ins.IsInitialized()) Refresh();
        }
    }
}