using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Root
{
    public class ChangeLenguajeButton : MonoBehaviour
    {
        // Método que se llamará desde el botón OnClick
        public void OnChangeLanguageButtonClicked()
        {
            if (Localization.Ins == null) return;

            // Alternar idioma
            var current = GetCurrentLanguage();
            var next = current == Languages.Spanish ? Languages.English : Languages.Spanish;
            SetCurrentLanguage(next);

            // Forzar actualización
            Localization.Ins.ForceUpdate();
        }

        // Métodos auxiliares para acceder al campo privado _currentLanguage
        Languages GetCurrentLanguage()
        {
            var field = typeof(Localization).GetField("_currentLanguage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (Languages)field.GetValue(Localization.Ins);
        }

        void SetCurrentLanguage(Languages lang)
        {
            var field = typeof(Localization).GetField("_currentLanguage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field.SetValue(Localization.Ins, lang);
        }
    }
}