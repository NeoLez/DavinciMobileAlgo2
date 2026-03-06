using Root.Gameplay.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Root
{
    public class StatPanel : MonoBehaviour {
        [SerializeField] private TMP_Text statName;
        [SerializeField] private TMP_Text value;
        [SerializeField] private Image image;

        public void Set(StatValue statValue) {
            statName.text = statValue.stat.statDescription;
            value.text = statValue.value.ToString();
            image.sprite = statValue.stat.statIcon;
        }
    }
}
