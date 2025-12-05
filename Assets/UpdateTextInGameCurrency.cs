using System.Collections;
using System.Collections.Generic;
using Root.Gameplay;
using TMPro;
using UnityEngine;

namespace Root
{
    [RequireComponent(typeof(TMP_Text))]
    public class UpdateTextInGameCurrency : MonoBehaviour
    {
        private TMP_Text text;
        void Start()
        {
            text = GetComponent<TMP_Text>();
        }

        void Update()
        {
            text.text = Level.Ins.gold.GetGold().ToString();
        }
    }
}
