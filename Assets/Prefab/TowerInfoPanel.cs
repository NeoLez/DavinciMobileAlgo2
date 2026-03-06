using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Root;
using Root.Gameplay.Stats;

namespace Root.UI
{
    public class TowerInfoPanel : MonoBehaviour
    {
        [Header("Textos Principales")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image towerIcon;

        [Header("Textos de Estadisticas (Adentro de StatsList)")]
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private TMP_Text fireRateText;
        [SerializeField] private TMP_Text rangeText;
        [SerializeField] private TMP_Text pierceText;

        [Header("Archivos Stat SO (Arrastrar desde el proyecto)")]
        [SerializeField] private StatSO damageStatSO;
        [SerializeField] private StatSO fireRateStatSO;
        [SerializeField] private StatSO rangeStatSO;
        [SerializeField] private StatSO pierceStatSO;

        public void MostrarInfo(GameObject torreSeleccionada, TowerSO datosDeLaTorre)
        {
            if (torreSeleccionada == null || datosDeLaTorre == null) return;

            gameObject.SetActive(true);

            if (titleText != null) titleText.text = datosDeLaTorre.towerName;
            if (descriptionText != null) descriptionText.text = datosDeLaTorre.description;
            if (towerIcon != null) towerIcon.sprite = datosDeLaTorre.icon;

            Stats towerStats = torreSeleccionada.GetComponent<Stats>();

            if (towerStats != null)
            {
                // Uso mi funcion ayudante para actualizar cada texto facilísimo
                ActualizarTextoStat(towerStats, damageStatSO, damageText, "Daño: ");
                ActualizarTextoStat(towerStats, fireRateStatSO, fireRateText, "Vel. Ataque: ");
                ActualizarTextoStat(towerStats, rangeStatSO, rangeText, "Rango: ");
                ActualizarTextoStat(towerStats, pierceStatSO, pierceText, "Perforación: ");
            }
            else
            {
                Debug.LogWarning("La torre seleccionada no tiene el componente Stats colgado.");
            }
        }

        
        private void ActualizarTextoStat(Stats towerStats, StatSO statSO, TMP_Text textComponent, string prefijo)
        {
            if (textComponent == null || statSO == null) return;

            StatValue valor = towerStats.GetValue(statSO);

            if (valor != null)
            {
                textComponent.gameObject.SetActive(true); 
                textComponent.text = prefijo + valor.value.ToString(); 
            }
            else
            {
                textComponent.gameObject.SetActive(false); // Si no tiene esta stat, oculto el renglón
            }
        }

        public void CerrarPanel()
        {
            gameObject.SetActive(false);
        }
    }
}