using Root.Gameplay;
using TMPro;
using UnityEngine;

namespace Root
{
    [RequireComponent(typeof(TMP_Text))]
    public class UpdateHealth : MonoBehaviour
    {
        private TMP_Text text;
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        void Update()
        {
            text.text = Level.Ins.playerHealth.ToString();
        }
    }
}
